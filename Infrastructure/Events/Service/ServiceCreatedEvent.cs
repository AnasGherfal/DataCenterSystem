using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Service;

public class ServiceCreatedEvent: EventStore<ServiceCreatedEventData>
{
    protected ServiceCreatedEvent() { }

    public ServiceCreatedEvent(Guid userId, Guid aggregateId, ServiceCreatedEventData data) : base(userId, aggregateId, 1, data)
    {
    }
}

public class ServiceCreatedEventData : IEventData
{
    public string Name { get; set; } = string.Empty;
    public string AmountOfPower { get; set; } = string.Empty;
    public string AcpPort { get; set; } = string.Empty;
    public string Dns { get; set; } = string.Empty;
    public int MonthlyVisits { get; set; } = 0;
    public decimal Price { get; set; } = 0;
    [JsonIgnore]
    public EventType Type => EventType.ServiceCreated;
}