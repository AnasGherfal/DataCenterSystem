using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public class VisitTypeConfig : IEntityTypeConfiguration<VisitType>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VisitType> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Name).IsRequired();
    }
}
