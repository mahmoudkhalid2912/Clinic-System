using ClinicManagementSystem.Domain.Abstractions;
using ClinicManagementSystem.Domain.Abstractions.IUnitOfWork;
using ClinicManagementSystem.Domain.Entities.Enums;
using ClinicManagementSystem.Domain.Errors;
using ClinicManagementSystem.Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using Stripe;

namespace ClinicManagementSystem.Application.Commands.Payment.StripeWebhook;

public class StripeWebhookCommandHandler(
    IUnitOfWork unitOfWork,
    IOptions<StripeSettings> stripeOptions)
    : IRequestHandler<StripeWebhookCommand, Result>
{
    public async Task<Result> Handle(StripeWebhookCommand request, CancellationToken cancellationToken)
    {
        Event stripeEvent;

        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                request.JsonPayload,
                request.StripeSignature,
                stripeOptions.Value.WebhookSecret);
        }
        catch
        {
            return await Task.FromResult(Result.Failure(PaymentError.InvalidWebhookSignature));
        }

        if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
        {
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            if (paymentIntent is null)
                return await Task.FromResult(Result.Success());

            var payment = await unitOfWork.PaymentRepository.GetAsync(p => p.StripePaymentIntentId == paymentIntent.Id, cancellationToken);
            if (payment is null)
                return await Task.FromResult(Result.Success()); // not our booking, ignore      
            // Update payment fields
            payment.PaidAmount = paymentIntent.Amount / 100m; // Stripe uses cents
            payment.RemainingAmount = payment.DueAmount - payment.PaidAmount;
            payment.PaymentDate = DateTime.UtcNow;
            payment.Method = paymentIntent.PaymentMethodTypes?.FirstOrDefault() ?? "card";
            payment.StripeStatus = "succeeded";
            unitOfWork.PaymentRepository.Update(payment);

            // Update booking status -> Confirmed
            var booking = await unitOfWork.BookinRepository.GetAsync(b => b.Id == payment.BookingId, cancellationToken);
            if (booking is not null)
            {
                booking.Status = BookingStatus.Confirmed;
                unitOfWork.BookinRepository.Update(booking);
            }

            unitOfWork.Save();
        }
        else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
        {
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            if (paymentIntent is not null)
            {
                var payment = await unitOfWork.PaymentRepository.GetAsync(p => p.StripePaymentIntentId == paymentIntent.Id, cancellationToken);
                if (payment is not null)
                {
                    payment.StripeStatus = "failed";
                    unitOfWork.PaymentRepository.Update(payment);
                    unitOfWork.Save();
                }
            }
        }

        return await Task.FromResult(Result.Success());
    }
}
