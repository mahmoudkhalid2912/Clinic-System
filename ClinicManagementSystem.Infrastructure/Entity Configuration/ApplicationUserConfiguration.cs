using ClinicManagementSystem.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagementSystem.Infrastructure.Entity_Configuration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.OwnsMany(u => u.RefreshTokens).ToTable("RefreshTokens")
            .WithOwner().HasForeignKey("UserId");
        builder.Property(u=>u.FullName)
            .HasMaxLength(150)
            .IsRequired();
    }
}
