using ClinicManagementSystem.Domain.Abstractions.IRepository.ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Domain.Entities;

namespace ClinicManagementSystem.Domain.Abstractions.IRepository;

public interface IBookingRepository:IGeneralRepository<Booking>
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

    Task<Result> AddBookingSafeAsync(Booking booking, CancellationToken cancellationToken);
}