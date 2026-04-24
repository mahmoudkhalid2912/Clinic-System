using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Domain.Entities.Enums;

namespace ClinicManagementSystem.Domain.Abstractions.IRepository;

public interface IBookingRepository
{
    Task AddAsync(Booking booking);

    Task<List<Booking>> GetByDateAsync(
        Guid scheduleId,
        DateTime date,
        CancellationToken cancellationToken);

    Task<bool> IsSlotTaken(
        Guid scheduleId,
        DateTime date,
        TimeSpan time,
        CancellationToken cancellationToken);
}