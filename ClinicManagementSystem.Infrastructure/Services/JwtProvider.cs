using ClinicManagementSystem.Application.Abstractions.Authentication;
using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.Services;

public class JwtProvider(IOptions<JwtSettings> options) : IJwtProvider
{
    private readonly JwtSettings _options = options.Value;

    public (string Token, int ExpireIn) GenerateToken(ApplicationUserDto applicationUserDto)
    {
        Claim[] claims =
        [
            new (JwtRegisteredClaimNames.Sub, applicationUserDto.Id),
            new (JwtRegisteredClaimNames.Email, applicationUserDto.Email ?? string.Empty),
            new (JwtRegisteredClaimNames.UniqueName, applicationUserDto.FullName),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        var signingCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpiryInMinutes),
            signingCredentials: signingCredentials
        );  
        return (new JwtSecurityTokenHandler().WriteToken(token), _options.ExpiryInMinutes*60);
    }

      public string? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        try
        {
            tokenHandler.ValidateToken(token, validationParameters: new TokenValidationParameters
            {
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
            return userId;
        }
        catch
        {
            return null;
        }
    }

}
