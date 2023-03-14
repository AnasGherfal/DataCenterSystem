using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class CompanionConfig : IEntityTypeConfiguration<Companion>
{
    public void Configure(EntityTypeBuilder<Companion> builder)
    {
        builder.Property(p =>p.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(50).IsRequired();
        builder.Property(p => p.FullName).HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
        builder.Property(p => p.IdentityNo).HasMaxLength(20).IsRequired();
        builder.Property(p => p.IdentityType).IsRequired();
        builder.Property(p => p.JobTitle).HasMaxLength(100);
        builder.Property(p => p.VisitId).IsRequired();


    }
}
