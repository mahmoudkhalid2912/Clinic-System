using ClinicManagementSystem.Domain.Entities.Enums;

namespace ClinicManagementSystem.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }

    public DateTime BookingDate { get; set; } = DateTime.UtcNow;

    public DateTime AppointmentDate { get; set; }
    public TimeSpan AppointmentTime { get; set; }

     
    public TimeSpan Duration { get; set; }
    public BookingStatus Status { get; set; }

    public string BookedByUserId { get; set; } = string.Empty;

    public string PatientName { get; set; }

    public string PatientPhone { get; set; }

    public string ? Notes { get; set; }
    public Guid ScheduleId { get; set; }

    public Payment? Payment { get; set; }



}
