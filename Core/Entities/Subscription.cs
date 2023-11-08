using Core.Constants;
using Core.Entities.Mappers;
using Core.Events.Subscription;

namespace Core.Entities;

public class Subscription: Entity
{
    public Guid Id { get; set; }
    public Guid ServiceId { get; set; }
    public Guid CustomerId { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    public DateTime ContractDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan MonthlyVisits { get; set; }
    public decimal TotalPrice { get; set; }
    public GeneralStatus Status { get; set; }
    // 
    public ICollection<DocumentForSubscription> Documents { get; set; } = new List<DocumentForSubscription>();
    public Service? Service { get; set; } = default!;
    public Customer? Customer { get; set; } = default!;
    
    public void Apply(SubscriptionCreatedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        Id = @event.AggregateId;
        ServiceId = @event.Data.ServiceId;
        CustomerId = @event.Data.CustomerId;
        ContractNumber = @event.Data.ContractNumber;
        ContractDate = @event.Data.ContractDate;
        StartDate = @event.Data.StartDate;
        EndDate = @event.Data.EndDate;
        Status = GeneralStatus.Active;
        TotalPrice = 0;
        MonthlyVisits = new TimeSpan(3,0,0);
        Documents.Add(new DocumentForSubscription()
        {
            SubscriptionId = @event.AggregateId,
            Id = @event.Data.Documents.FileIdentifier,
            FileLink = @event.Data.Documents.FileLink,
            FileType = @event.Data.Documents.FileType,
            IsActive = true,
            CreatedOn = @event.DateTime,
        });
    }
    
    public void Apply(SubscriptionRequestedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        Id = @event.AggregateId;
        ServiceId = @event.Data.ServiceId;
        CustomerId = @event.Data.CustomerId;
        StartDate = @event.Data.StartDate;
        EndDate = @event.Data.EndDate;
        Status = GeneralStatus.Requested;
        TotalPrice = 0;
        MonthlyVisits = new TimeSpan(3,0,0);
        Documents.Add(new DocumentForSubscription()
        {
            SubscriptionId = @event.AggregateId,
            Id = @event.Data.Documents.FileIdentifier,
            FileLink = @event.Data.Documents.FileLink,
            FileType = @event.Data.Documents.FileType,
            IsActive = true,
            CreatedOn = @event.DateTime,
        });
    }
    
    public void Apply(SubscriptionFileUpdatedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        foreach (var document in Documents)
        {
            document.IsActive = false;
        }
        Documents.Add(new DocumentForSubscription()
        {
            SubscriptionId = @event.AggregateId,
            Id = @event.Data.FileIdentifier,
            FileLink = @event.Data.FileLink,
            FileType = @event.Data.FileType,
            IsActive = true,
            CreatedOn = @event.DateTime,
        });
    }
    
    public void Apply(SubscriptionRenewedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        EndDate = @event.DateTime.AddYears(1);
    }
    
    
    public void Apply(SubscriptionLockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Locked;
    }
    
    public void Apply(SubscriptionApprovedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Active;
    }
    
    public void Apply(SubscriptionRejectedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Rejected;
    }
    
    public void Apply(SubscriptionUnlockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Active;
    }
    
    public void Apply(SubscriptionDeletedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        IsDeleted = true;
    }
}