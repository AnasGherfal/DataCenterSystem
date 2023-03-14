using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public class VisitTimeShiftConfig : IEntityTypeConfiguration<VisitTimeShift>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VisitTimeShift> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Property(p => p.PriceForFirstHour).HasPrecision(5,2).IsRequired();
        builder.Property(p => p.PriceForRemainingHour).HasPrecision(5,2).IsRequired();
        builder.Property(p => p.StartTime).IsRequired();
        builder.Property(p => p.EndTime).IsRequired();
        builder.Property(p => p.Status).IsRequired();

    }
}
