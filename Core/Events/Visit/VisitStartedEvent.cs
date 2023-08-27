using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Visit;

public class VisitStartedEvent: EventStore<VisitStartedEventData>
{
    protected VisitStartedEvent() { }

    public VisitStartedEvent(Guid userId, Guid aggregateId, long sequence, VisitStartedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class VisitStartedEventData : IEventData
{
    public DateTime StartTime { get; set; }
    [JsonIgnore]
    public EventType Type => EventType.VisitStarted;
}