using ClinicManagementSystem.Domain.Abstractions;
using ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Domain.Entities.Enums;
using ClinicManagementSystem.Domain.Errors;
using ClinicManagementSystem.Infrastructure.Persistence;
using ClinicManagementSystem.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
{
    private readonly ClinicDbContext _context;

    public BookingRepository(ClinicDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
    }

    // ✔ FIXED: date filtering (safe range)
    public async Task<List<Booking>> GetByDateAsync(
        Guid scheduleId,
        DateTime date,
        CancellationToken cancellationToken)
    {
        var start = date.Date;
        var end = start.AddDays(1);

        return await _context.Bookings
            .Where(b =>
                b.ScheduleId == scheduleId &&
                b.AppointmentDate >= start &&
                b.AppointmentDate < end)
            .ToListAsync(cancellationToken);
    }

    // ✔ FIXED: slot check (production-safe)
    public async Task<bool> IsSlotTaken(
        Guid scheduleId,
        DateTime date,
        TimeSpan time,
        CancellationToken cancellationToken)
    {
        var start = date.Date;
        var end = start.AddDays(1);

        return await _context.Bookings.AnyAsync(b =>
            b.ScheduleId == scheduleId &&
            b.AppointmentDate >= start &&
            b.AppointmentDate < end &&
            b.AppointmentTime == time &&
            (
                b.Status == BookingStatus.Confirmed ||
                (b.Status == BookingStatus.Pending && b.ExpiresAt > DateTime.UtcNow)
            ),
            cancellationToken);
    }

    public async Task<Result> AddBookingSafeAsync(
    Booking booking, Payment payment,
    CancellationToken cancellationToken)
    {
        try
        {
            await _context.Bookings.AddAsync(booking, cancellationToken);
            await _context.Payments.AddAsync(payment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (DbUpdateException)
        {
            return Result.Failure(BookingError.SlotAlreadyBooked);
        }
    }

    
}