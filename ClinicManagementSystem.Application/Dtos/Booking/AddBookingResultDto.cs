namespace ClinicManagementSystem.Application.Dtos.Booking;

public class AddBookingResultDto
{
    public Guid BookingId { get; set; }

    public DateTime ExpiresAt { get; set; }

}
