using Infrastructure.Constants;
using Infrastructure.Events.Service;

namespace Infrastructure.Entities;

public class Service: Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AmountOfPower { get; set; } = string.Empty;
    public string AcpPort { get; set; } = string.Empty;
    public string Dns { get; set; } = string.Empty;
    public int MonthlyVisits { get; set; }
    public decimal Price { get; set; }
    public GeneralStatus Status { get; set; }
    
    public void Apply(ServiceCreatedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        Id = @event.AggregateId;
        Name = @event.Data.Name;
        AmountOfPower = @event.Data.AmountOfPower;
        AcpPort = @event.Data.AcpPort;
        Dns = @event.Data.Dns;
        MonthlyVisits = @event.Data.MonthlyVisits;
        Price = @event.Data.Price;
        Status = GeneralStatus.Active;
    }
    
    public void Apply(ServiceUpdatedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Name = @event.Data.Name;
        AmountOfPower = @event.Data.AmountOfPower;
        AcpPort = @event.Data.AcpPort;
        Dns = @event.Data.Dns;
        MonthlyVisits = @event.Data.MonthlyVisits;
        Price = @event.Data.Price;
    }
    
    public void Apply(ServiceLockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Locked;
    }
    
    public void Apply(ServiceUnlockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = GeneralStatus.Active;
    }
    
    public void Apply(ServiceDeletedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        IsDeleted = true;
    }
}