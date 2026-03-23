using ClinicManagementSystem.Application.Abstractions.Authentication;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;


namespace ClinicManagementSystem.Application.Commands.Authentication.ResetPassword;

public class ResetPasswordCommandHandler(IAuthenticationService authService) : IRequestHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    =>await authService.ResetPasswordAsync(request, cancellationToken);
}

