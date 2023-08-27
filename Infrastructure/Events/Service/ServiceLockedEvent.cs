using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Service;

public class ServiceLockedEvent: EventStore<ServiceLockedEventData>
{
    protected ServiceLockedEvent() { }

    public ServiceLockedEvent(Guid userId, Guid aggregateId, long sequence, ServiceLockedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class ServiceLockedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.ServiceLocked;
}