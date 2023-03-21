namespace Infrastructure.Configurations;

public class SubscriptionConfig : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.Property(p => p.StartDate).IsRequired();
        builder.Property(p => p.EndDate).IsRequired();
        builder.Property(p => p.TotalPrice).HasPrecision(10,2).IsRequired();
        builder.Property(p => p.SubscriptionFileId).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.CustomerId).IsRequired();
        builder.Property(p => p.ServiceId).IsRequired();
        builder.HasOne(a => a.CreatedBy)
               .WithMany(u => u.SubscriptionsCreatedBy)
               .HasForeignKey(a => a.CreatedById)
               .OnDelete(DeleteBehavior.ClientSetNull);
       

    }
}
