using ClinicManagementSystem.Application.Commands.Authentication.Login;
using ClinicManagementSystem.Application.Commands.Authentication.RefreshToken;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var response = await mediator.Send(command);

        return response.IsSuccess
            ? Ok(response.Value)
            : response.ToSimpleError();
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var response = await mediator.Send(command);

        return response.IsSuccess
            ? Ok(response.Value)
            : response.ToSimpleError();
    }

    [HttpPost("revokeRefreshToken")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenCommand command)
    {
        var response = await mediator.Send(command);

        return response.IsSuccess
            ? Ok()
            : response.ToSimpleError();
    }
}