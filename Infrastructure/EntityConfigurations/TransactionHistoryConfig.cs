using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.EntityConfigurations;

public class TransactionHistoryConfig : IEntityTypeConfiguration<TransactionHistory>
{
    public void Configure(EntityTypeBuilder<TransactionHistory> builder)
    {
        builder.Property(p => p.Action).IsRequired();
        builder.Property(p => p.EntityId).HasMaxLength(150).IsRequired();
        builder.Property(p => p.EntityType).IsRequired();
        builder.Property(p => p.EntityData).IsRequired();
    }
}

