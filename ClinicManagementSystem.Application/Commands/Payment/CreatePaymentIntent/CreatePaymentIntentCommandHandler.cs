using ClinicManagementSystem.Application.Abstractions.Payment;
using ClinicManagementSystem.Application.Dtos.Payment;
using ClinicManagementSystem.Domain.Abstractions;
using ClinicManagementSystem.Domain.Abstractions.IUnitOfWork;
using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Domain.Errors;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Payment.CreatePaymentIntent;

public class CreatePaymentIntentCommandHandler(
    IUnitOfWork unitOfWork,
    IStripeService stripeService)
    : IRequestHandler<CreatePaymentIntentCommand, Result<PaymentIntentResponseDto>>
{
    public async Task<Result<PaymentIntentResponseDto>> Handle(
        CreatePaymentIntentCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Find the booking
        var booking = unitOfWork.BookinRepository.Get(b => b.Id == request.BookingId);
        if (booking is null)
            return Result.Failure<PaymentIntentResponseDto>(PaymentError.BookingNotFound);

        // 2. Find its payment record
        var payment = unitOfWork.PaymentRepository.Get(p => p.BookingId == request.BookingId);
        if (payment is null)
            return Result.Failure<PaymentIntentResponseDto>(PaymentError.PaymentNotFound);

        // 3. Check not already fully paid
        if (payment.RemainingAmount <= 0 && payment.StripeStatus == "succeeded")
            return Result.Failure<PaymentIntentResponseDto>(PaymentError.AlreadyPaid);

        // 4. Create Stripe PaymentIntent
        var stripeResult = await stripeService.CreatePaymentIntentAsync(
            payment.DueAmount,
            request.Currency,
            request.BookingId.ToString(),
            cancellationToken);

        if (stripeResult.IsFailuer)
            return Result.Failure<PaymentIntentResponseDto>(stripeResult.Error);

        // clientSecret format: "pi_xxx_secret_yyy" — we extract the PaymentIntent ID
        var clientSecret = stripeResult.Value;
        var paymentIntentId = clientSecret.Split('_')[0] + "_" + clientSecret.Split('_')[1]; // e.g. "pi_3xxx"

        // 5. Persist the PaymentIntent ID on the Payment record
        payment.StripePaymentIntentId = paymentIntentId;
        payment.StripeStatus = "requires_payment_method";
        unitOfWork.PaymentRepository.Update(payment);
        unitOfWork.Save();

        return Result.Success(new PaymentIntentResponseDto
        {
            ClientSecret = clientSecret,
            PaymentIntentId = paymentIntentId,
            Amount = payment.DueAmount,
            Currency = request.Currency
        });
    }
}
