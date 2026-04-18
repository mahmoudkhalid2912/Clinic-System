namespace ClinicManagementSystem.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }

    public string Method { get; set; } = string.Empty;

    public decimal DueAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount { get; set; }

    public DateTime PaymentDate { get; set; }

    public Guid BookingId { get; set; }
    public Booking Booking { get; set; }



}