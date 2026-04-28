using ClinicManagementSystem.API.Extensions;
using ClinicManagementSystem.Application.Commands.Booking;
using ClinicManagementSystem.Application.Query.Booking;
using ClinicManagementSystem.Domain.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.API.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public BookingController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    // 🟢 Get available slots
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

    
    [HttpPost("add-booking")]
    public async Task<IActionResult> AddBooking([FromBody] AddBookingCommand command)
    {
        var userid = User.GetUserId();
        command.BookedUserId = userid;
        var result = await _mediator.Send(command);

        return result.ToApiResponse("Booking created successfully you have 10 minutes to Pay");
    }
}