using ClinicManagementSystem.Application.Dtos.Booking;
using ClinicManagementSystem.Domain.Abstractions;
using ClinicManagementSystem.Domain.Abstractions.IUnitOfWork;
using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Domain.Entities.Enums;
using ClinicManagementSystem.Domain.Errors;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Booking;

public class AddBookingCommandHandler
    : IRequestHandler<AddBookingCommand, Result<AddBookingResultDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddBookingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AddBookingResultDto>> Handle(
        AddBookingCommand request,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        // 1. Get schedule
        var schedule = await _unitOfWork.ScheduleRepository
            .GetByDateAsync(request.AppointmentDate.Date, cancellationToken);

        if (schedule is null)
            return Result.Failure<AddBookingResultDto>(BookingError.NoScheduleFound);

        // 2. Validate slot range
        var slotDateTime = request.AppointmentDate.Date.Add(request.AppointmentTime);

        if (request.AppointmentTime < schedule.StartTime ||
            request.AppointmentTime + schedule.SlotDuration > schedule.EndTime)
        {
            return Result.Failure<AddBookingResultDto>(BookingError.InvalidSlot);
        }

        // 3. Prevent past booking
        if (slotDateTime < now)
            return Result.Failure<AddBookingResultDto>(BookingError.PastAppointmentNotAllowed);

        // 4. Build entity
        var booking = new Domain.Entities.Booking
        {
            Id = Guid.NewGuid(),
            ScheduleId = schedule.Id,
            AppointmentDate = request.AppointmentDate.Date,
            AppointmentTime = request.AppointmentTime,
            Duration = schedule.SlotDuration,
            PatientName = request.PatientName,
            PatientPhone = request.PatientPhone,
            Notes = request.Notes,
            BookedByUserId = request.BookedUserId,
            Status = BookingStatus.Pending,
            BookingDate = now,
            ExpiresAt = now.AddMinutes(10)
        };

        var Payment = new Domain.Entities.Payment
        {
            Id = Guid.NewGuid(),
            BookingId = booking.Id,

        };
        

        // 5. Save safely (handled in Infrastructure)
        var result = await _unitOfWork.BookinRepository
            .AddBookingSafeAsync(booking, cancellationToken);

        if (result.IsFailuer)
            return Result.Failure<AddBookingResultDto>(result.Error);

        // 6. Return response
        return Result.Success(new AddBookingResultDto
        {
            BookingId = booking.Id,
            ExpiresAt = booking.ExpiresAt
        });
    }
}