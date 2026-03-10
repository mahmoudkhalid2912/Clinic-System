using Microsoft.AspNetCore.Identity;

namespace ClinicManagementSystem.Infrastructure.Identity;

public class ApplicationUser: IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public List<RefreshToken> RefreshTokens { get; set; } = [];
}
