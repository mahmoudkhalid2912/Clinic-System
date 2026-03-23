using ClinicManagementSystem.Application.Abstractions.Authentication;
using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;


namespace ClinicManagementSystem.Application.Commands.Authentication.ForgetPassword;

public class FrogetPasswordCommandHandler(IAuthenticationService authService) : IRequestHandler<ForgetPasswordCommand, Result<ForgetPasswordResponse>>
{
    private readonly IAuthenticationService _authService = authService;

    public async Task<Result<ForgetPasswordResponse>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    => await _authService.SendResetPasswordEmailAsync(request.Email,cancellationToken);
}
