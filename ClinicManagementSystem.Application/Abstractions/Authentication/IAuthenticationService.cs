using ClinicManagementSystem.Application.Commands.Authentication.Register;
using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Abstractions;

namespace ClinicManagementSystem.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    
    Task<Result<LoginResponseDto?>> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);

    
    Task<Result<LoginResponseDto?>> GenerateRefreshTokenasync(
        string token,
        string refreshToken,
        CancellationToken cancellationToken = default);

    
    Task<Result<bool>> RevokeRefreshTokenasync(
        string token,
        string refreshToken,
        CancellationToken cancellationToken = default);

    
    Task<Result<LoginResponseDto?>> RegisterAsync(
        RegisterCommand request,
        CancellationToken cancellationToken = default);
}