using Core.Constants;
using Core.Entities.Mappers;
using Core.Events.Invoice;
using Core.Events.Visit;

namespace Core.Entities;

public class Visit : Entity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid SubscriptionId { get; set; }
    public Guid? InvoiceId { get; set; }
    public VisitType VisitType { get; set; }
    public DateTime ExpectedStartTime { get; set; }
    public DateTime ExpectedEndTime { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? TotalTime { get; set; }
    public string Notes { get; set; } = string.Empty;
    public decimal VisitPrice { get; set; }
    public GeneralStatus Status { get; set; }
    public ICollection<RepresentativeForVisit> Representatives { get; set; } = new List<RepresentativeForVisit>();
    public ICollection<CompanionForVisit> Companions { get; set; } = new List<CompanionForVisit>();
    public Subscription? Subscription { get; set; }
    public Customer? Customer { get; set; }
    
    public void Apply(VisitCreatedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        Id = @event.AggregateId;
        CustomerId = @event.Data.CustomerId;
        SubscriptionId = @event.Data.SubscriptionId;
        VisitType = @event.Data.VisitType;
        ExpectedStartTime = @event.Data.ExpectedStartTime;
        ExpectedEndTime = @event.Data.ExpectedEndTime;
        Notes = @event.Data.Notes;
        Representatives = @event.Data.Representatives.Select(x => new RepresentativeForVisit()
        {
            Id = Guid.NewGuid(),
            VisitId = @event.AggregateId,
            RepresentativeId = x,
        }).ToList();
        Companions = @event.Data.Companions.Select(x => new CompanionForVisit()
        {
            Id = Guid.NewGuid(),
            FirstName = x.FirstName,
            LastName = x.LastName,
            IdentityNo = x.IdentityNo,
            IdentityType = x.IdentityType,
            JobTitle = x.JobTitle
        }).ToList();
        Status = GeneralStatus.Active;
        VisitPrice = 0;
    }
    
    public void Apply(VisitRequestedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        Id = @event.AggregateId;
        CustomerId = @event.Data.CustomerId;
        SubscriptionId = @event.Data.SubscriptionId;
        VisitType = @event.Data.VisitType;
        ExpectedStartTime = @event.Data.ExpectedStartTime;
        ExpectedEndTime = @event.Data.ExpectedEndTime;
        Notes = @event.Data.Notes;
        Representatives = @event.Data.Representatives.Select(x => new RepresentativeForVisit()
        {
            Id = Guid.NewGuid(),
            VisitId = @event.AggregateId,
            RepresentativeId = x,
        }).ToList();
        Companions = @event.Data.Companions.Select(x => new CompanionForVisit()
        {
            Id = Guid.NewGuid(),
            FirstName = x.FirstName,
            LastName = x.LastName,
            IdentityNo = x.IdentityNo,
            IdentityType = x.IdentityType,
            JobTitle = x.JobTitle
        }).ToList();
        Status = GeneralStatus.Requested;
        VisitPrice = 0;
    }
    
    public void Apply(VisitSignedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        StartTime = @event.Data.StartTime;
        EndTime = @event.Data.EndTime;
        TotalTime = @event.Data.TotalTime;
        VisitPrice = @event.Data.Price;
    }
    public void Apply(VisitStartedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        StartTime = @event.Data.StartTime;
    }
    
    public void Apply(VisitEndedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        EndTime = @event.Data.EndTime;
        TotalTime = @event.Data.TotalTime;
        VisitPrice = @event.Data.Price;
    }
    
    public void Apply(VisitCancelledEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        IsDeleted = true;
    }
    
    public void Apply(VisitDeletedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        IsDeleted = true;
    }
    
    //Non-Event Sourced
    public void Apply(InvoiceCreatedEvent @event)
    {
        InvoiceId = @event.AggregateId;
    }
}