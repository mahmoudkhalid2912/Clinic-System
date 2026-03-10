using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Authentication.RefreshToken;

public class RevokeRefreshTokenCommand: IRequest<Result<bool>>
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; }=string.Empty;
}
