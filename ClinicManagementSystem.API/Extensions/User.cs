namespace ClinicManagementSystem.API.Extensions;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


public static class User
{
    public static string? GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}

