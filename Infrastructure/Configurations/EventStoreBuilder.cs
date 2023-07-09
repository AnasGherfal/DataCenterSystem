using Infrastructure.Audits.Abstracts;
using Infrastructure.Audits.Admin;
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
                .HasValue<AdminPasswordResetAudit>(AuditType.AdminPasswordReset);
            b.ToTable("AuditStore");
        });
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminCreatedAudit, AdminCreatedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminUpdatedAudit, AdminUpdatedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminDeletedAudit, AdminDeletedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminLockedAudit, AdminLockedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminUnlockedAudit, AdminUnlockedAuditData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminPasswordResetAudit, AdminPasswordResetAuditData>());
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