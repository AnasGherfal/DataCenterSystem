using Core.Constants;
using Core.Entities.Mappers;
using Core.Events.Representative;

namespace Core.Entities;

public class Representative: Entity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public RepresentativeType RepresentativeType { get; set; }
    public DateTime? ActiveFrom { get; set; }
    public DateTime? ActiveTo { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentityNo { get; set; } = string.Empty;
    public IdentityType IdentityType { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public GeneralStatus Status { get; set; }
    // 
    public ICollection<DocumentForRepresentative> Documents { get; set; } = new List<DocumentForRepresentative>();
    public Customer? Customer { get; set; } = default!;
    
    public void Apply(RepresentativeCreatedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        Id = @event.AggregateId;
        CustomerId = @event.Data.CustomerId;
        RepresentativeType = @event.Data.RepresentativeType;
        ActiveFrom = @event.Data.ActiveFrom;
        ActiveTo = @event.Data.ActiveTo;
        FirstName = @event.Data.FirstName;
        LastName = @event.Data.LastName;
        IdentityNo = @event.Data.IdentityNo;
        IdentityType = @event.Data.IdentityType;
        Email = @event.Data.Email;
        PhoneNo = @event.Data.PhoneNo;
        Status = GeneralStatus.Active;
        Documents = @event.Data.Documents.Select(p => new DocumentForRepresentative()
        {
            RepresentativeId = @event.AggregateId,
            Id = p.FileIdentifier,
            FileLink = p.FileLink,
            FileType = p.FileType,
            IsActive = true,
            CreatedOn = @event.DateTime,
        }).ToList();
    }
    
    public void Apply(RepresentativeRequestedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        Id = @event.AggregateId;
        CustomerId = @event.Data.CustomerId;
        FirstName = @event.Data.FirstName;
        LastName = @event.Data.LastName;
        IdentityNo = @event.Data.IdentityNo;
        IdentityType = @event.Data.IdentityType;
        Email = @event.Data.Email;
        PhoneNo = @event.Data.PhoneNo;
        Status = GeneralStatus.Requested;
        Documents = @event.Data.Documents.Select(p => new DocumentForRepresentative()
        {
            RepresentativeId = @event.AggregateId,
            Id = p.FileIdentifier,
            FileLink = p.FileLink,
            FileType = p.FileType,
            IsActive = true,
            CreatedOn = @event.DateTime,
        }).ToList();
    }
    
    public void Apply(RepresentativeFileUpdatedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        var document = Documents.SingleOrDefault(p => p.Id == @event.Data.OldFileIdentifier);
        Documents.Remove(document!);
        Documents.Add(new DocumentForRepresentative()
        {
            RepresentativeId = @event.AggregateId,
            Id = @event.Data.FileIdentifier,
            FileLink = @event.Data.FileLink,
            FileType = @event.Data.FileType,
            IsActive = true,
            CreatedOn = @event.DateTime,
        });
    }

    public void Apply(RepresentativeLockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Locked;
    }
    
    public void Apply(RepresentativeUnlockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Active;
    }
    
    public void Apply(RepresentativeApprovedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Active;
    }
    
    public void Apply(RepresentativeRejectedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Rejected;
    }
    
    public void Apply(RepresentativeDeletedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        IsDeleted = true;
    }
    public void Apply(RepresentativeUpdatedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        IdentityType = @event.Data.IdentityType;
        IdentityNo = @event.Data.IdentityNo;
        Email = @event.Data.Email;
        PhoneNo = @event.Data.PhoneNo;
    }
}