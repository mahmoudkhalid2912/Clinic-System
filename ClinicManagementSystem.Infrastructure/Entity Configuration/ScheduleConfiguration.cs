using ClinicManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Entity_Configuration;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Schedule> builder)
    {builder.Property(s => s.Day)
               .HasConversion<string>();

        builder.HasIndex(s => s.Day)
               .IsUnique();
    }
}
