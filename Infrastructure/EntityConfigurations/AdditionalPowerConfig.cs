using Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public class AdditionalPowerConfig : IEntityTypeConfiguration<AdditionalPower>
{
    public void Configure(EntityTypeBuilder<AdditionalPower> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Power).HasMaxLength(64).IsRequired();
        builder.Property(p => p.Price).HasPrecision(10, 2).IsRequired();

    }
}


