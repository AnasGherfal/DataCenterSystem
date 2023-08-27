using Infrastructure.Models;
namespace Infrastructure.Configurations;

public class VisitTypeConfig : IEntityTypeConfiguration<VisitType>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VisitType> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Name).IsRequired();
    }
}
