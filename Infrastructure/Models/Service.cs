using Infrastructure.Constants;
using Infrastructure.Events.Service;

namespace Infrastructure.Models;

public class Service : BaseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AmountOfPower { get; set; } = string.Empty;
    public string AcpPort { get; set; } = string.Empty;
    public string Dns { get; set; } = string.Empty;
    public int MonthlyVisits { get; set; }
    public decimal Price { get; set; }
    public GeneralStatus Status { get; set; }
    //------------Ralation
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    
    
    public void Apply(ServiceCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Name = @event.Data.Name;
        AmountOfPower = @event.Data.AmountOfPower;
        AcpPort = @event.Data.AcpPort;
        Dns = @event.Data.Dns;
        MonthlyVisits = @event.Data.MonthlyVisits;
        Price = @event.Data.Price;
        Status = GeneralStatus.Active;
        CreatedOn = @event.DateTime;
        CreatedById = @event.UserId;
    }
    
    public void Apply(ServiceUpdatedEvent @event)
    {
        Name = @event.Data.Name;
        AmountOfPower = @event.Data.AmountOfPower;
        AcpPort = @event.Data.AcpPort;
        Dns = @event.Data.Dns;
        MonthlyVisits = @event.Data.MonthlyVisits;
        Price = @event.Data.Price;
    }
    
    public void Apply(ServiceLockedEvent @event)
    {
        Status = GeneralStatus.Locked;
    }
    
    public void Apply(ServiceUnlockedEvent @event)
    {
        Status = GeneralStatus.Active;
    }
    
    public void Apply(ServiceDeletedEvent @event)
    {
        Status = GeneralStatus.Deleted;
    }
}
