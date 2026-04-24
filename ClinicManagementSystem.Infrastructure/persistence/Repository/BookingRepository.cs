using ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Domain.Entities.Enums;
using ClinicManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Persistence.Repository;

public class BookingRepository : IBookingRepository
{
    private readonly ClinicDbContext _context;

    public BookingRepository(ClinicDbContext context)
    {
        _context = context;
    }

    // ➤ Add booking
    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
    }

    // ➤ Get all bookings for a specific schedule & date
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

    // ➤ Check if slot is already booked
    public async Task<bool> IsSlotTaken(
        Guid scheduleId,
        DateTime date,
        TimeSpan time,
        CancellationToken cancellationToken)
    {
        return await _context.Bookings.AnyAsync(b =>
            b.ScheduleId == scheduleId &&
            b.AppointmentDate == date.Date &&
            b.AppointmentTime == time &&
            b.Status != BookingStatus.Cancelled,
            cancellationToken);
    }
}