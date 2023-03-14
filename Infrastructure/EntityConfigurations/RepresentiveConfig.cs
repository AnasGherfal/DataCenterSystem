using Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public class RepresentiveConfig : IEntityTypeConfiguration<Representive>
{
    public void Configure(EntityTypeBuilder<Representive> builder)
    {
        builder.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(50).IsRequired();
        builder.Property(p => p.FullName).HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
        builder.Property(p => p.IdentityNo).HasMaxLength(20).IsRequired();
        builder.Property(p => p.IdentityType).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(320).IsRequired();
        builder.Property(p => p.PhoneNo).HasMaxLength(20).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.CustomerId).IsRequired();
    }
}
