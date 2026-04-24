using ClinicManagementSystem.API.Extensions;
using ClinicManagementSystem.Application.Query.Booking;
using ClinicManagementSystem.Domain.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClinicManagementSystem.API.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class BookingController(IMediator mediator, IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet("available-slots")]
    public async Task<IActionResult> GetAvailableSlots([FromQuery] DateTime date)
    {
        var query = new GetAvailableAppointmentsQuery
        {
            Date = date
        };

        var result = await _mediator.Send(query);

        return result.ToApiResponse("Available slots retrieved successfully");

    }

   
}
