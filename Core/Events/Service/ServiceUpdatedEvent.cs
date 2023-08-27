using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Service;

public class ServiceUpdatedEvent: EventStore<ServiceUpdatedEventData>
{
    protected ServiceUpdatedEvent() { }

    public ServiceUpdatedEvent(Guid userId, Guid aggregateId, long sequence, ServiceUpdatedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class ServiceUpdatedEventData : IEventData
{
    public string Name { get; set; } = string.Empty;
    public string AmountOfPower { get; set; } = string.Empty;
    public string AcpPort { get; set; } = string.Empty;
    public string Dns { get; set; } = string.Empty;
    public int MonthlyVisits { get; set; } = 0;
    public decimal Price { get; set; } = 0;
    [JsonIgnore]
    public EventType Type => EventType.ServiceUpdated;
}