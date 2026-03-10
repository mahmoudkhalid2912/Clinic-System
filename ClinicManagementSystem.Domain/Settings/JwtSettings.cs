using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Domain.Settings;

public class JwtSettings
{
    public static string SectionName = "Jwt";

    [Required(ErrorMessage = "Issuer is required")]
    public string Issuer { get; set; } = string.Empty;

    [Required(ErrorMessage = "Audience is required")]
    public string Audience { get; set; } = string.Empty;

    [Required(ErrorMessage = "SecretKey is required")]
    public string SecretKey { get; set; } = string.Empty;

    [Required(ErrorMessage = "ExpiryInMinutes is required")]
    [Range(1, 1440, ErrorMessage = "ExpiryInMinutes must be between 1 minute and 24 hours (1440 minutes)")]
    public int ExpiryInMinutes { get; set; }
}