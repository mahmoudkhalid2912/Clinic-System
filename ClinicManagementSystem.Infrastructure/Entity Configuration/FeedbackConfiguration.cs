using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Entity_Configuration;

public class FeedbackConfiguration: IEntityTypeConfiguration<Feedback>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.PatientName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(f => f.PatientPhone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(f => f.Comment)
            .HasMaxLength(1000);

        builder.Property(f => f.Rating)
            .IsRequired();

        // PatientId is now nullable — feedback can be submitted without login
        builder.HasOne<ApplicationUser>()
               .WithMany()
               .HasForeignKey(f => f.PatientId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
