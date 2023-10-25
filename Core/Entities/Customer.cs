using Core.Constants;
using Core.Entities.Mappers;
using Core.Events.Customer;

namespace Core.Entities;

public class Customer: Account
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PrimaryPhone { get; set; } = string.Empty;
    public string SecondaryPhone { get; set; } = string.Empty;
    public GeneralStatus Status { get; set; }
    //
    public ICollection<DocumentForCustomer> Documents { get; set; } = new List<DocumentForCustomer>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public ICollection<Representative> Representatives { get; set; } = new List<Representative>();
    
    public void Apply(CustomerCreatedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        Id = @event.AggregateId;
        Name = @event.Data.Name;
        Address = @event.Data.Address;
        City = @event.Data.City;
        PrimaryPhone = @event.Data.PrimaryPhone;
        SecondaryPhone = @event.Data.SecondaryPhone;
        Email = @event.Data.Email;
        Status = GeneralStatus.Active;
        Documents = @event.Data.Documents.Select(p => new DocumentForCustomer()
        {
            Id = p.FileIdentifier,
            CustomerId = @event.AggregateId,
            FileLink = p.FileLink,
            FileType = p.FileType,
            IsActive = true,
            CreatedOn = @event.DateTime,
        }).ToList();
    }
    public void Apply(CustomerUpdatedEvent @event)
    {
        Name=@event.Data.Name;
        Address = @event.Data.Address;
        PrimaryPhone=@event.Data.PrimaryPhone;
        SecondaryPhone = @event.Data.SecondaryPhone;
        City = @event.Data.City;
        Email= @event.Data.Email;
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Active;

    }

    public void Apply(CustomerLockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Locked;
    }
    
    public void Apply(CustomerUnlockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Active;
    }
    
    public void Apply(CustomerDeletedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        IsDeleted = true;
    }
    
    public void Apply(CustomerFileUpdatedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        if (@event.Data.OldFileIdentifier != null)
        {
            var document = Documents.SingleOrDefault(p => p.Id == @event.Data.OldFileIdentifier);
            Documents.Remove(document!);
        }
        Documents.Add(new DocumentForCustomer()
        {
            CustomerId = @event.AggregateId,
            Id = @event.Data.FileIdentifier,
            FileLink = @event.Data.FileLink,
            FileType = @event.Data.FileType,
            IsActive = true,
            CreatedOn = @event.DateTime,
        });
    }
}