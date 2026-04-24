using ClinicManagementSystem.Domain.Entities.Enums;

namespace ClinicManagementSystem.Domain.Entities;

public class Schedule
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }
    public WeekDay Day { get; set; }

    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public TimeSpan SlotDuration { get; set; }
}
