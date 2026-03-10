using ClinicManagementSystem.Application.Abstractions.Authentication;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Authentication.RefreshToken;

public class RevokedRefreshTokenHandler(IAuthenticationService authservice) : IRequestHandler<RevokeRefreshTokenCommand, Result<bool>>
{
    private readonly IAuthenticationService _authservice = authservice;

    public async Task<Result<bool>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    => await _authservice.RevokeRefreshTokenasync(request.Token, request.RefreshToken, cancellationToken);
}
