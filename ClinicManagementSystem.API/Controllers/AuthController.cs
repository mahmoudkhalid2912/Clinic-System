using ClinicManagementSystem.Application.Commands.Authentication.Login;
using ClinicManagementSystem.Application.Commands.Authentication.RefreshToken;
using ClinicManagementSystem.Application.Commands.Authentication.Register;
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
}