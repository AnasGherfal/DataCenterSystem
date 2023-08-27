using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Service;

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