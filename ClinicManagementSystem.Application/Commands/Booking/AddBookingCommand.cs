using ClinicManagementSystem.Application.Dtos.Booking;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Booking;

public class AddBookingCommand: IRequest<Result<AddBookingResultDto>>
{
    public DateTime AppointmentDate { get; set; }

    public TimeSpan AppointmentTime { get; set; }

    public string PatientName { get; set; } = string.Empty;

    public string PatientPhone { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public string BookedUserId {  get; set; }= string.Empty;
}
