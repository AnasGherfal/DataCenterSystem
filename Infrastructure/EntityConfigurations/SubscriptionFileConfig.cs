using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.EntityConfigurations;

public class SubscriptionFileConfig : IEntityTypeConfiguration<SubscriptionFile>
{
    public void Configure(EntityTypeBuilder<SubscriptionFile> builder)
    {
        builder.HasOne(a => a.Subscription)
            .WithOne(a => a.SubscriptionFile)
            .HasForeignKey<SubscriptionFile>(c => c.SubscriptionId);
    }
}

