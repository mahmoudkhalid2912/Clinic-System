using ClinicManagementSystem.Application.Dtos.Booking;
using ClinicManagementSystem.Domain.Abstractions;
using ClinicManagementSystem.Domain.Abstractions.IUnitOfWork;
using ClinicManagementSystem.Domain.Entities.Enums;
using ClinicManagementSystem.Domain.Errors;
using MediatR;

namespace ClinicManagementSystem.Application.Query.Booking;

public class GetAvailableAppointmentsQueryHandler
    : IRequestHandler<GetAvailableAppointmentsQuery, Result<List<AvailableSlotDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAvailableAppointmentsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<AvailableSlotDto>>> Handle(
        GetAvailableAppointmentsQuery request,
        CancellationToken cancellationToken)
    {
        var date = request.Date.Date;

        var schedule = await _unitOfWork.ScheduleRepository
            .GetByDateAsync(date, cancellationToken);

        if (schedule is null)
            return Result.Failure<List<AvailableSlotDto>>(BookingError.NoScheduleFound);

        var bookings = await _unitOfWork.BookinRepository
            .GetByDateAsync(schedule.Id, date, cancellationToken);

        var bookedTimes = bookings
            .Where(b => b.Status != BookingStatus.Cancelled)
            .Select(b => b.AppointmentTime)
            .ToHashSet();

        var slots = new List<AvailableSlotDto>();

        var current = schedule.StartTime;
        var now = DateTime.UtcNow;

        while (current + schedule.SlotDuration <= schedule.EndTime)
        {
            var isBooked = bookedTimes.Contains(current);

            var isPast =
                date == now.Date &&
                current <= now.TimeOfDay;

            if (!isBooked && !isPast)
            {
                slots.Add(new AvailableSlotDto
                {
                    Time = current
                });
            }

            current = current.Add(schedule.SlotDuration);
        }

        return Result.Success(slots);
    }
}