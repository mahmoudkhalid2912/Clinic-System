using ClinicManagementSystem.API.Extensions;
using ClinicManagementSystem.Application.Commands.User.ChangePassword;
using ClinicManagementSystem.Application.Query.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.API.Controllers;

[Route("me")]
[ApiController]
[Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetUserProfile()
    {
        var userId = User.GetUserId();

        var response = await _mediator.Send(
            new GetUserProfileQuery { UserId = userId! }
        );

        return response.ToApiResponse("User Profile");
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand request)
    {
        var userId = User.GetUserId();
        request.UserId = userId!;
        var response = await _mediator.Send(request);
        return response.ToApiResponse("Change Password Succefully");
    }
}