using ClinicManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Entity_Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Payment> builder)
    {

        builder.Property(p => p.DueAmount)
               .HasPrecision(18, 2);

        builder.Property(p => p.PaidAmount)
               .HasPrecision(18, 2);

        builder.Property(p => p.RemainingAmount)
               .HasPrecision(18, 2);
    }
}
