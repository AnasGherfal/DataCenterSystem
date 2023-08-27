using Infrastructure.Models;
namespace Infrastructure.Configurations;

public class TransactionHistoryConfig : IEntityTypeConfiguration<TransactionHistory>
{
    public void Configure(EntityTypeBuilder<TransactionHistory> builder)
    {
        builder.Property(p => p.Action).IsRequired();
        // builder.Property(p => p.EntityId).HasMaxLength(150).IsRequired();
        builder.Property(p => p.EntityType).IsRequired();
        builder.Property(p => p.EntityData).IsRequired();
        builder.HasOne(a => a.CreatedBy)
               .WithMany(u => u.TransactionHistorysCreatedBy)
               .HasForeignKey(a => a.CreatedById)
               .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

