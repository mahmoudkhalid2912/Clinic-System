using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Entity_Configuration;

public class MedicalHistoryConfiguration : IEntityTypeConfiguration<MedicalHistory>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MedicalHistory> builder)
    {
        builder.HasOne<ApplicationUser>()
               .WithOne()
               .HasForeignKey<MedicalHistory>(m => m.PatientId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}