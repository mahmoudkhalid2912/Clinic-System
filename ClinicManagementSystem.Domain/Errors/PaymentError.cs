using ClinicManagementSystem.Domain.Abstractions;

namespace ClinicManagementSystem.Domain.Errors;

public static class PaymentError
{
    public static readonly Error BookingNotFound = new(
        "Payment.BookingNotFound",
        "Booking not found.",
        404);

    public static readonly Error PaymentNotFound = new(
        "Payment.PaymentNotFound",
        "Payment record not found for this booking.",
        404);

    public static readonly Error AlreadyPaid = new(
        "Payment.AlreadyPaid",
        "This booking has already been paid.",
        400);

    public static readonly Error StripeError = new(
        "Payment.StripeError",
        "An error occurred while processing the payment with Stripe.",
        500);

    public static readonly Error InvalidWebhookSignature = new(
        "Payment.InvalidWebhookSignature",
        "Invalid Stripe webhook signature.",
        400);
}
