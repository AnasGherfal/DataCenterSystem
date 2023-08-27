using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Service;

public class ServiceDeletedEvent: EventStore<ServiceDeletedEventData>
{
    protected ServiceDeletedEvent() { }

    public ServiceDeletedEvent(Guid userId, Guid aggregateId, long sequence, ServiceDeletedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class ServiceDeletedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.ServiceDeleted;
}