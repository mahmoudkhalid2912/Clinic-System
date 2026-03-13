using ClinicManagementSystem.Application.Abstractions.Authentication;
using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Authentication.Register;

public class RegisterCommandHandler(IAuthenticationService authService) : IRequestHandler<RegisterCommand, Result<LoginResponseDto?>>
{
    private readonly IAuthenticationService _authService = authService;

    public async Task<Result<LoginResponseDto?>> Handle(RegisterCommand request, CancellationToken cancellationToken)
     => await _authService.RegisterAsync(request, cancellationToken);
}
