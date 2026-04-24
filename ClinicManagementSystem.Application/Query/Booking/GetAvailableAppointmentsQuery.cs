using ClinicManagementSystem.Application.Dtos.Booking;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Query.Booking;

public class GetAvailableAppointmentsQuery:IRequest<Result<List<AvailableSlotDto>>>
{
    public DateTime Date { get; set; }
}
