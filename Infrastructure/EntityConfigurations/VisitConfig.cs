using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public class VisitConfig : IEntityTypeConfiguration<Visit>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Visit> builder)
    {
        builder.Property(p => p.Price).HasPrecision(6,2).IsRequired();
        builder.Property(p => p.Notes).HasMaxLength(300);
        builder.Property(p => p.VisitTypeId).IsRequired();
        builder.Property(p => p.VisitShiftId).IsRequired();
        


    }
}
