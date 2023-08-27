using Infrastructure.Models;
namespace Infrastructure.Configurations;

public class AdditionalPowerConfig : IEntityTypeConfiguration<AdditionalPower>
{
    public void Configure(EntityTypeBuilder<AdditionalPower> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Power).HasMaxLength(64).IsRequired();
        builder.Property(p => p.Price).HasPrecision(10, 2).IsRequired();
        builder.HasOne(a => a.CreatedBy)
               .WithMany(u => u.AdditionalPowersCreatedBy)
               .HasForeignKey(a => a.CreatedById)
               .OnDelete(DeleteBehavior.ClientSetNull);
    }
}


