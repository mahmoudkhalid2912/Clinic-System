using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Entity_Configuration;

public class FeedbackConfiguration: IEntityTypeConfiguration<Feedback>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Feedback> builder)
    {
        builder.HasOne<ApplicationUser>()
              .WithMany()
              .HasForeignKey(f => f.PatientId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
