using ClinicManagementSystem.Domain.Abstractions;

namespace ClinicManagementSystem.Application.Abstractions.Payment;

public interface IStripeService
{
    /// <summary>
    /// Creates a Stripe PaymentIntent and returns the client_secret.
    /// </summary>
    Task<Result<string>> CreatePaymentIntentAsync(decimal amount, string currency, string bookingId, CancellationToken cancellationToken = default);
}
