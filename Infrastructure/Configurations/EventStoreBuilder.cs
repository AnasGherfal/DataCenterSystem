using Infrastructure.Audits.Abstracts;
using Infrastructure.Audits.Admin;
using Infrastructure.Audits.Service;
using Infrastructure.Audits.Subscription;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Configurations;

public static class AuditBuilder
{
    public static void AddAuditBuilder(this DataCenterContext dbContext,  ModelBuilder builder)
    {
        builder.Entity<Audit>(b => {
            b.HasKey(p => p.Id);
            b.HasIndex(p => new
            {
                p.AggregateId,
                p.DateTime,
            }).IsUnique();
            b.HasDiscriminator(e => e.Type)
                .HasValue<AdminCreatedAudit>(AuditType.AdminCreated)
                .HasValue<AdminUpdatedAudit>(AuditType.AdminUpdated)
                .HasValue<AdminDeletedAudit>(AuditType.AdminDeleted)
                .HasValue<AdminLockedAudit>(AuditType.AdminLocked)
                .HasValue<AdminUnlockedAudit>(AuditType.AdminUnlocked)
                .HasValue<AdminPasswordResetAudit>(AuditType.AdminPasswordReset)
                .HasValue<ServiceCreatedAudit>(AuditType.ServiceCreated)
                .HasValue<ServiceUpdatedAudit>(AuditType.ServiceUpdated)
                .HasValue<ServiceDeletedAudit>(AuditType.ServiceDeleted)
                .HasValue<ServiceLockedAudit>(AuditType.ServiceLocked)
                .HasValue<ServiceUnlockedAudit>(AuditType.ServiceUnlocked)
                .HasValue<SubscriptionCreatedAudit>(AuditType.SubscriptionCreated)
                .HasValue<SubscriptionRenewedAudit>(AuditType.SubscriptionRenewed)
                .HasValue<SubscriptionDeletedAudit>(AuditType.SubscriptionDeleted)
                .HasValue<SubscriptionLockedAudit>(AuditType.SubscriptionLocked)
                .HasValue<SubscriptionUnlockedAudit>(AuditType.SubscriptionUnlocked)
                
                ;
            b.ToTable("AuditStore");
        });
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminCreatedAudit, AdminCreatedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminUpdatedAudit, AdminUpdatedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminDeletedAudit, AdminDeletedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminLockedAudit, AdminLockedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminUnlockedAudit, AdminUnlockedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminPasswordResetAudit, AdminPasswordResetAuditData>());
        
        builder.ApplyConfiguration(new EventStoreConfiguration<ServiceCreatedAudit, ServiceCreatedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<ServiceUpdatedAudit, ServiceUpdatedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<ServiceDeletedAudit, ServiceDeletedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<ServiceLockedAudit, ServiceLockedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<ServiceUnlockedAudit, ServiceUnlockedAuditData>());
        
        builder.ApplyConfiguration(new EventStoreConfiguration<SubscriptionCreatedAudit, SubscriptionCreatedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<SubscriptionRenewedAudit, SubscriptionRenewedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<SubscriptionDeletedAudit, SubscriptionDeletedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<SubscriptionLockedAudit, SubscriptionLockedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<SubscriptionUnlockedAudit, SubscriptionUnlockedAuditData>());
        
    }
}

public class EventStoreConfiguration<T, TData> : IEntityTypeConfiguration<T> where T : AuditStore<TData> where TData : IAuditData
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(e => e.Data).HasConversion(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<TData>(v)!
        ).HasColumnName("Content");
    }
}