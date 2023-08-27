using Infrastructure.Entities;

namespace Infrastructure.Builders;

public static class SubscriptionBuilder
{
    public static void AddSubscriptionBuilder(this DbContext dbContext,  ModelBuilder builder)
    {
        builder.Entity<Subscription>(b => {
            b.HasQueryFilter(p => !p.IsDeleted);
            b.HasKey(p => p.Id);
            b.Property(p => p.CreatedOn).IsRequired().HasDefaultValue(DateTime.UtcNow);
            b.Property(p => p.UpdatedOn).IsRequired().HasDefaultValue(DateTime.UtcNow);
            b.Property(p => p.Sequence).IsRequired().HasDefaultValue(1);
            b.Property(a => a.RowVersion).IsRequired().IsRowVersion();
            b.Property(a => a.IsDeleted).IsRequired().HasDefaultValue(false);
            b.ToTable("Subscriptions");
        });
    }
}