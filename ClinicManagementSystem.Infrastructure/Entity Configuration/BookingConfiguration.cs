using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Entity_Configuration;

public class BookingConfiguration: IEntityTypeConfiguration<Booking>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Booking> builder)
    {
        builder.Property(b => b.Status)
                .IsRequired()
                .HasConversion<string>();

        builder.HasOne(b => b.Payment)
               .WithOne(p => p.Booking)
               .HasForeignKey<Payment>(p => p.BookingId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<ApplicationUser>()
               .WithMany(u => u.Bookings)
               .HasForeignKey(b => b.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Schedule>()
               .WithMany()
               .HasForeignKey(b => b.ScheduleId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(b => new { b.ScheduleId, b.AppointmentDate, b.AppointmentTime })
               .IsUnique();
    }
}
