using System;
using System.Collections.Generic;
using System.Text;


namespace ClinicManagementSystem.Domain.Entities;

public class Feedback
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public string PatientName { get; set; } = string.Empty;

    public string PatientPhone { get; set; } = string.Empty;

    public string Comment { get; set; } = string.Empty;

    public int Rating { get; set; }

    public string? PatientId { get; set; }


}
