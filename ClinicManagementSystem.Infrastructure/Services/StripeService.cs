using ClinicManagementSystem.Application.Abstractions.Payment;
using ClinicManagementSystem.Domain.Abstractions;
using ClinicManagementSystem.Domain.Errors;
using ClinicManagementSystem.Domain.Settings;
using Microsoft.Extensions.Options;
using Stripe;

namespace ClinicManagementSystem.Infrastructure.Services;

public class StripeService(IOptions<StripeSettings> stripeOptions) : IStripeService
{
    public async Task<Result<string>> CreatePaymentIntentAsync(
        decimal amount,
        string currency,
        string bookingId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            StripeConfiguration.ApiKey = stripeOptions.Value.SecretKey;

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // convert to cents
                Currency = currency.ToLower(),
                Metadata = new Dictionary<string, string>
                {
                    { "bookingId", bookingId }
                },
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                    AllowRedirects = "never"
                },
            };

            var service = new PaymentIntentService();
            var intent = await service.CreateAsync(options, cancellationToken: cancellationToken);

            return Result.Success(intent.ClientSecret);
        }
        catch (StripeException ex)
        {
            return Result.Failure<string>(new Error("Stripe.ApiError", ex.Message, 500));
        }
    }
}
