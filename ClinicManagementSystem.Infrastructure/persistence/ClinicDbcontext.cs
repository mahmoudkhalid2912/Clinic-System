using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Persistence
{
    public class ClinicDbContext(DbContextOptions<ClinicDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
       
        public DbSet<Booking> Bookings { get; set; }   
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Report> Reports { get; set; }      
        public DbSet<PatientRecord> PatientRecords { get; set; }

        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUser).Assembly);
            base.OnModelCreating(builder);
           
        }
    }
}