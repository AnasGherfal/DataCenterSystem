using Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public class ServiceConfig : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.Property(p => p.AmountOfPower).HasMaxLength(100).IsRequired();
        builder.Property(p => p.AcpPort).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Dns).HasMaxLength(100).IsRequired();
        builder.Property(p => p.MonthlyVisits).IsRequired();
        builder.Property(p => p.Price).HasPrecision(8,2).IsRequired();
        builder.Property(p => p.Status).IsRequired();
    }
}
