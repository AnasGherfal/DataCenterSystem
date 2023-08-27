using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Service;

public class ServiceUnlockedEvent: EventStore<ServiceUnlockedEventData>
{
    protected ServiceUnlockedEvent() { }

    public ServiceUnlockedEvent(Guid userId, Guid aggregateId, long sequence, ServiceUnlockedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class ServiceUnlockedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.ServiceUnlocked;
}