using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Authentication.RefreshToken;

public class RefreshTokenCommand:IRequest<Result<LoginResponseDto?>>
{
    public string Token { get; set; }=string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
