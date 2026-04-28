namespace ClinicManagementSystem.Application.Dtos.Booking;

public class AvailableSlotDto
{
    public TimeSpan Time { get; set; }
    public bool IsAvailable { get; set; }

}
