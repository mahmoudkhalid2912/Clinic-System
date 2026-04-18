using ClinicManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Infrastructure.Identity;

public class ApplicationUser: IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public List<RefreshToken> RefreshTokens { get; set; } = [];


    [RegularExpression(@"^\d{6}$", ErrorMessage = "Reset password code must be 6 digits")]
    public string? ResetPasswordCode { get; set; }

    public DateTime? ResetPasswordCodeExpiry { get; set; }

    public ICollection<Booking> Bookings { get; set; }
}
