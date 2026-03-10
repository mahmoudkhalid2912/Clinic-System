using ClinicManagementSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Persistence
{
    public class ClinicDbContext(DbContextOptions<ClinicDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationUser).Assembly);
            base.OnModelCreating(builder);
           
        }
    }
}