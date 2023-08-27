using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Infrastructure.Events.Admin;
using Infrastructure.Events.Customer;
using Infrastructure.Events.Invoice;
using Infrastructure.Events.Representative;
using Infrastructure.Events.Service;
using Infrastructure.Events.Subscription;
using Infrastructure.Events.TimeShift;
using Infrastructure.Events.Visit;
using Newtonsoft.Json;

namespace Infrastructure.Builders;

public static class EventStoreBuilder
{
    public static void AddEventBuilder(this DbContext dbContext,  ModelBuilder builder)
    {
        builder.Entity<Event>(b => {
            b.HasKey(p => p.Id);
            b.HasIndex(p => new
            {
                p.AggregateId,
                p.DateTime,
            }).IsUnique();
            b.HasDiscriminator(e => e.Type)
                .HasValue<AdminCreatedEvent>(EventType.AdminCreated)
                .HasValue<AdminUpdatedEvent>(EventType.AdminUpdated)
                .HasValue<AdminDeletedEvent>(EventType.AdminDeleted)
                .HasValue<AdminLockedEvent>(EventType.AdminLocked)
                .HasValue<AdminUnlockedEvent>(EventType.AdminUnlocked)
                .HasValue<AdminPasswordResetEvent>(EventType.AdminPasswordReset)
                .HasValue<AdminPasswordChangedEvent>(EventType.AdminPasswordChanged)
                .HasValue<ServiceCreatedEvent>(EventType.ServiceCreated)
                .HasValue<ServiceUpdatedEvent>(EventType.ServiceUpdated)
                .HasValue<ServiceDeletedEvent>(EventType.ServiceDeleted)
                .HasValue<ServiceLockedEvent>(EventType.ServiceLocked)
                .HasValue<ServiceUnlockedEvent>(EventType.ServiceUnlocked)
                .HasValue<SubscriptionCreatedEvent>(EventType.SubscriptionCreated)
                .HasValue<SubscriptionRenewedEvent>(EventType.SubscriptionRenewed)
                .HasValue<SubscriptionDeletedEvent>(EventType.SubscriptionDeleted)
                .HasValue<SubscriptionLockedEvent>(EventType.SubscriptionLocked)
                .HasValue<SubscriptionUnlockedEvent>(EventType.SubscriptionUnlocked)
                .HasValue<SubscriptionFileUpdatedEvent>(EventType.SubscriptionFileUpdated)
                .HasValue<RepresentativeCreatedEvent>(EventType.RepresentativeCreated)
                .HasValue<RepresentativeUpdatedEvent>(EventType.RepresentativeUpdated)
                .HasValue<RepresentativeDeletedEvent>(EventType.RepresentativeDeleted)
                .HasValue<RepresentativeLockedEvent>(EventType.RepresentativeLocked)
                .HasValue<RepresentativeUnlockedEvent>(EventType.RepresentativeUnlocked)
                .HasValue<RepresentativeFileUpdatedEvent>(EventType.RepresentativeFileUpdated)
                .HasValue<CustomerCreatedEvent>(EventType.CustomerCreated)
                .HasValue<CustomerUpdatedEvent>(EventType.CustomerUpdated)
                .HasValue<CustomerDeletedEvent>(EventType.CustomerDeleted)
                .HasValue<CustomerLockedEvent>(EventType.CustomerLocked)
                .HasValue<CustomerUnlockedEvent>(EventType.CustomerUnlocked)
                .HasValue<CustomerFileUpdatedEvent>(EventType.CustomerFileUpdated)
                
                .HasValue<TimeShiftCreatedEvent>(EventType.TimeShiftCreated)
                .HasValue<TimeShiftUpdatedEvent>(EventType.TimeShiftUpdated)
                .HasValue<TimeShiftDeletedEvent>(EventType.TimeShiftDeleted)
                
                .HasValue<VisitCreatedEvent>(EventType.VisitCreated)
                .HasValue<VisitStartedEvent>(EventType.VisitStarted)
                .HasValue<VisitEndedEvent>(EventType.VisitEnded)
                .HasValue<VisitDeletedEvent>(EventType.VisitDeleted)
                .HasValue<InvoiceCreatedEvent>(EventType.InvoiceCreated)
                .HasValue<InvoicePaidEvent>(EventType.InvoicePaid)
                ;
            b.ToTable("EventStore");
        });
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminCreatedEvent, AdminCreatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminUpdatedEvent, AdminUpdatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminDeletedEvent, AdminDeletedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminLockedEvent, AdminLockedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminUnlockedEvent, AdminUnlockedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminPasswordResetEvent, AdminPasswordResetEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<AdminPasswordChangedEvent, AdminPasswordChangedEventData>());
        
        builder.ApplyConfiguration(new EventStoreConfiguration<ServiceCreatedEvent, ServiceCreatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<ServiceUpdatedEvent, ServiceUpdatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<ServiceDeletedEvent, ServiceDeletedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<ServiceLockedEvent, ServiceLockedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<ServiceUnlockedEvent, ServiceUnlockedEventData>());
        
        builder.ApplyConfiguration(new EventStoreConfiguration<SubscriptionCreatedEvent, SubscriptionCreatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<SubscriptionRenewedEvent, SubscriptionRenewedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<SubscriptionDeletedEvent, SubscriptionDeletedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<SubscriptionLockedEvent, SubscriptionLockedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<SubscriptionUnlockedEvent, SubscriptionUnlockedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<SubscriptionFileUpdatedEvent, SubscriptionFileUpdatedEventData>());
        
        builder.ApplyConfiguration(new EventStoreConfiguration<RepresentativeCreatedEvent, RepresentativeCreatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<RepresentativeUpdatedEvent, RepresentativeUpdatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<RepresentativeDeletedEvent, RepresentativeDeletedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<RepresentativeLockedEvent, RepresentativeLockedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<RepresentativeUnlockedEvent, RepresentativeUnlockedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<RepresentativeFileUpdatedEvent, RepresentativeFileUpdatedEventData>());
    
        builder.ApplyConfiguration(new EventStoreConfiguration<CustomerCreatedEvent, CustomerCreatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<CustomerUpdatedEvent, CustomerUpdatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<CustomerDeletedEvent, CustomerDeletedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<CustomerLockedEvent, CustomerLockedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<CustomerUnlockedEvent, CustomerUnlockedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<CustomerFileUpdatedEvent, CustomerFileUpdatedEventData>());
        
        
        
        builder.ApplyConfiguration(new EventStoreConfiguration<TimeShiftCreatedEvent, TimeShiftCreatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<TimeShiftUpdatedEvent, TimeShiftUpdatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<TimeShiftDeletedEvent, TimeShiftDeletedEventData>());
            
        builder.ApplyConfiguration(new EventStoreConfiguration<VisitCreatedEvent, VisitCreatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<VisitStartedEvent, VisitStartedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<VisitEndedEvent, VisitEndedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<VisitDeletedEvent, VisitDeletedEventData>());
        
        builder.ApplyConfiguration(new EventStoreConfiguration<InvoiceCreatedEvent, InvoiceCreatedEventData>());
        builder.ApplyConfiguration(new EventStoreConfiguration<InvoicePaidEvent, InvoicePaidEventData>());
    }
}

public class EventStoreConfiguration<T, TData> : IEntityTypeConfiguration<T> where T : EventStore<TData> where TData : IEventData
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(e => e.Data).HasConversion(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<TData>(v)!
        ).HasColumnName("Content");
    }
}