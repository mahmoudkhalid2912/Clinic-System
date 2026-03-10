namespace ClinicManagementSystem.Application.Dtos.Authentication;

public class LoginResponseDto
{
   public string Id { get; set; } = string.Empty;
   public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    public int ExpiresIn { get; set; }

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime RefreshTokenExpriation { get; set; }
}
