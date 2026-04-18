namespace ClinicManagementSystem.Domain.Entities;

public class MedicalHistory
{
    public Guid Id { get; set; }

    public string PatientId { get; set; } = string.Empty;

    public ICollection<PatientRecord> Records { get; set; } = new List<PatientRecord>();

}
