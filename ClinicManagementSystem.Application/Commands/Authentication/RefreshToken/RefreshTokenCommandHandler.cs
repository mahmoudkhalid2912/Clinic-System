using ClinicManagementSystem.Application.Abstractions.Authentication;
using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Authentication.RefreshToken;

public class RefreshTokenCommandHandler(IAuthenticationService authService) : IRequestHandler<RefreshTokenCommand, Result<LoginResponseDto?>>
{
    private readonly IAuthenticationService _authService = authService;
    public async Task<Result<LoginResponseDto?>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    => await _authService.GenerateRefreshTokenasync(request.Token, request.RefreshToken,cancellationToken);
}
