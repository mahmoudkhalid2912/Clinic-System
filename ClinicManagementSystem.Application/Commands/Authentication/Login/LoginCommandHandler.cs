using ClinicManagementSystem.Application.Abstractions.Authentication;
using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Authentication.Login;

public class LoginCommandHandler(IAuthenticationService authService) : IRequestHandler<LoginCommand, Result<LoginResponseDto?>>
{
    private readonly IAuthenticationService _authService = authService;

    public async Task<Result<LoginResponseDto?>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var res = await _authService.LoginAsync(request.Email, request.Password, cancellationToken);
          
        return res;
    }
}
