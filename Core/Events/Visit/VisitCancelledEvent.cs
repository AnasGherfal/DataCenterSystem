using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Visit;

public class VisitCancelledEvent: EventStore<VisitCancelledEventData>
{
    protected VisitCancelledEvent() { }

    public VisitCancelledEvent(Guid userId, Guid aggregateId, long sequence, VisitCancelledEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class VisitCancelledEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.VisitCancelled;
}