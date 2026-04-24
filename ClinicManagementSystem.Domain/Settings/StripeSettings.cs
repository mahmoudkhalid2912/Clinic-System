namespace ClinicManagementSystem.Domain.Settings;

public class StripeSettings
{
    public const string SectionName = "Stripe";

    public string SecretKey { get; set; } = string.Empty;     // sk_test_...
    public string PublishableKey { get; set; } = string.Empty; // pk_test_...
    public string WebhookSecret { get; set; } = string.Empty;  // whsec_...
}
