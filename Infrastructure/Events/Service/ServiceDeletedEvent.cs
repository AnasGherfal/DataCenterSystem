using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Service;

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