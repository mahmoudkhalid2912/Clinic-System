using ClinicManagementSystem.Application.Dtos.Authentication;

namespace ClinicManagementSystem.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    (string Token,int ExpireIn) GenerateToken(ApplicationUserDto applicationUserDto);
    string? ValidateToken(string token);
}
