using ClinicManagementSystem.Application.Commands.Authentication.Login;
using ClinicManagementSystem.Application.Commands.Authentication.RefreshToken;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginCommand command)
    {
         var validationResult = await new LoginCommandValidator().ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        var Response = await mediator.Send(command);
        return Response.IsSuccess ? Ok(Response.Value) : Response.ToSimpleError();
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenCommand command)
    {
        var validationResult = await new RefreshTokenCommandValidator().ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        var Response = await mediator.Send(command);
        return Response.IsSuccess ? Ok(Response.Value) : Response.ToSimpleError();
    }

    [HttpPost("revokeRefreshToken")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody]RevokeRefreshTokenCommand command)
    {
        var isRevoked = await mediator.Send(command);
        return isRevoked.IsSuccess ? Ok(isRevoked) : isRevoked.ToSimpleError();
    }
}