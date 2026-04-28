using ClinicManagementSystem.Application.Dtos.Booking;
using ClinicManagementSystem.Application.Query.Booking;
using ClinicManagementSystem.Domain.Abstractions;
using ClinicManagementSystem.Domain.Abstractions.IUnitOfWork;
using ClinicManagementSystem.Domain.Errors;
using MediatR;

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
        var now = DateTime.UtcNow.AddHours(3);

        var schedule = await _unitOfWork.ScheduleRepository
            .GetByDateAsync(date, cancellationToken);

        if (schedule is null)
            return Result.Failure<List<AvailableSlotDto>>(BookingError.NoScheduleFound);

        var slots = new List<AvailableSlotDto>();
        var current = schedule.StartTime;

        while (current + schedule.SlotDuration <= schedule.EndTime)
        {
            var slotDateTime = date.Add(current);

           
            var isBooked = await _unitOfWork.BookinRepository
                .IsSlotTaken(schedule.Id, date, current, cancellationToken);

            var isPast = slotDateTime < now;

            slots.Add(new AvailableSlotDto
            {
                Time = current,
                IsAvailable = !isBooked && !isPast
            });

            current = current.Add(schedule.SlotDuration);
        }

        return Result.Success(slots.OrderBy(s => s.Time).ToList());
    }
}