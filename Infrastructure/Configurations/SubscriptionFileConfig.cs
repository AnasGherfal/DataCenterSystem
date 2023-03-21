namespace Infrastructure.Configurations;

public class SubscriptionFileConfig : IEntityTypeConfiguration<SubscriptionFile>
{
    public void Configure(EntityTypeBuilder<SubscriptionFile> builder)
    {
        builder.HasOne(p=>p.Subscription)
               .WithOne(p=>p.SubscriptionFile)
               .HasForeignKey<SubscriptionFile>(p=>p.SubscriptionId)
               .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(a => a.CreatedBy)
                .WithMany(u => u.SubscriptionFilesCreatedBy)
                .HasForeignKey(a => a.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

