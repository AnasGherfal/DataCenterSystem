using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Visit;

public class VisitDeletedEvent: EventStore<VisitDeletedEventData>
{
    protected VisitDeletedEvent() { }

    public VisitDeletedEvent(Guid userId, Guid aggregateId, long sequence, VisitDeletedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class VisitDeletedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.VisitDeleted;
}