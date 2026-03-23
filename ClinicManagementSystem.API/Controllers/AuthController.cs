using ClinicManagementSystem.Application.Commands.Authentication.ForgetPassword;
using ClinicManagementSystem.Application.Commands.Authentication.Login;
using ClinicManagementSystem.Application.Commands.Authentication.RefreshToken;
using ClinicManagementSystem.Application.Commands.Authentication.Register;
using ClinicManagementSystem.Application.Commands.Authentication.ResetPassword;
using ClinicManagementSystem.Application.Commands.User.ChangePassword;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result.ToApiResponse("Login successfully");
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result.ToApiResponse("Token refreshed successfully");
    }

    [HttpPost("revokeRefreshToken")]
    public async Task<IActionResult> RevokeRefreshToken(
        [FromBody] RevokeRefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result.ToApiResponse("Refresh token revoked successfully");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result.ToApiResponse("Register successfully");
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword(
        [FromBody] ForgetPasswordCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToApiResponse("Check your email to reset your password");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(
        [FromBody] ResetPasswordCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.ToApiResponse("Password reset successfully");
    }
    
}