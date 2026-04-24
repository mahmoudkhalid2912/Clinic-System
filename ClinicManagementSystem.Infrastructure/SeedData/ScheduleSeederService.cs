using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Domain.Entities.Enums;
using ClinicManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.SeedData;

public class ScheduleSeederService
{
    private readonly ClinicDbContext _context;

    public ScheduleSeederService(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        var startDate = DateTime.UtcNow.Date;
        var endDate = startDate.AddDays(100);

        var existingDates = await _context.Schedules
            .Select(s => s.Date.Date)
            .ToListAsync();

        var schedules = new List<Schedule>();

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday)
                continue;

            if (existingDates.Contains(date.Date))
                continue;

            schedules.Add(new Schedule
            {
                Id = Guid.NewGuid(),
                Date = date.Date,
                Day = MapDay(date.DayOfWeek),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                SlotDuration = new TimeSpan(0, 30, 0)
            });
        }

        if (schedules.Count > 0)
        {
            await _context.Schedules.AddRangeAsync(schedules);
            await _context.SaveChangesAsync();
        }
    }

    private WeekDay MapDay(DayOfWeek day)
    {
        return day switch
        {
            DayOfWeek.Monday => WeekDay.Monday,
            DayOfWeek.Tuesday => WeekDay.Tuesday,
            DayOfWeek.Wednesday => WeekDay.Wednesday,
            DayOfWeek.Thursday => WeekDay.Thursday,
            DayOfWeek.Friday => WeekDay.Friday,
            DayOfWeek.Saturday => WeekDay.Saturday,
            DayOfWeek.Sunday => WeekDay.Sunday,
            _ => throw new Exception("Invalid day")
        };
    }
}