using ClinicManagementSystem.Domain.Entities.Enums;

namespace ClinicManagementSystem.Domain.Entities;

public class PatientRecord
{
    public Guid Id { get; set; }

    public string Medications { get; set; } = string.Empty;

    public int MedicationTimes { get; set; }

    public MedicineType MedicineType { get; set; }

    public string Symptoms { get; set; } = string.Empty;

    public string Diagnosis { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public Guid MedicalHistoryId { get; set; }

    public MedicalHistory MedicalHistory { get; set; }
}
