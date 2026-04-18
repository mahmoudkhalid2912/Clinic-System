using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Persistence
{
    public class ClinicDbContext(DbContextOptions<ClinicDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
       
        DbSet<Booking> Bookings { get; set; }   
        DbSet<Payment> Payments { get; set; }
        DbSet<Schedule> Schedules { get; set; }

        DbSet<Feedback> Feedbacks { get; set; }

        DbSet<Report> Reports { get; set; }

        DbSet<PatientRecord> PatientRecords { get; set; }

        DbSet<MedicalHistory> MedicalHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUser).Assembly);
            base.OnModelCreating(builder);
           
        }
    }
}