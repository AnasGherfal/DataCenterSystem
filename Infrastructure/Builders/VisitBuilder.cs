using Infrastructure.Entities;

namespace Infrastructure.Builders;

public static class VisitBuilder
{
    public static void AddVisitBuilder(this DbContext dbContext,  ModelBuilder builder)
    {
        builder.Entity<Visit>(b => {
            b.HasQueryFilter(p => !p.IsDeleted);
            b.HasKey(p => p.Id);
            b.Property(p => p.CreatedOn).IsRequired().HasDefaultValue(DateTime.UtcNow);
            b.Property(p => p.UpdatedOn).IsRequired().HasDefaultValue(DateTime.UtcNow);
            b.Property(p => p.Sequence).IsRequired().HasDefaultValue(1);
            b.Property(a => a.RowVersion).IsRequired().IsRowVersion();
            b.Property(a => a.IsDeleted).IsRequired().HasDefaultValue(false);
            b.HasOne(v => v.Subscription)
            .WithMany()
            .HasForeignKey(v => v.SubscriptionId)
            .OnDelete(DeleteBehavior.NoAction);
            b.ToTable("Visits");
        });
    }
}