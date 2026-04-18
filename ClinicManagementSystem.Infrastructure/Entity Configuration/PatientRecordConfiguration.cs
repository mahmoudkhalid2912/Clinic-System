using ClinicManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Entity_Configuration;

public class PatientRecordConfiguration : IEntityTypeConfiguration<PatientRecord>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PatientRecord> builder)
    {

        builder.HasOne(r => r.MedicalHistory)
              .WithMany(m => m.Records)
              .HasForeignKey(r => r.MedicalHistoryId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.Property(r => r.MedicineType)
               .HasConversion<string>();
    }
}
