using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Abstractions;

namespace ClinicManagementSystem.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<Result<LoginResponseDto?>> LoginAsync(string email, string password,CancellationToken cancellationToken);

    Task<Result<LoginResponseDto?>> GenerateRefreshTokenasync(string Token, string RefreshToken, CancellationToken cancellationToken);

    Task<Result<bool>> RevokeRefreshTokenasync(string Token,string RefreshToken, CancellationToken cancellationToken);
}
