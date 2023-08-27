using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Visit;

public class VisitEndedEvent: EventStore<VisitEndedEventData>
{
    protected VisitEndedEvent() { }

    public VisitEndedEvent(Guid userId, Guid aggregateId, long sequence, VisitEndedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class VisitEndedEventData : IEventData
{
    public DateTime EndTime { get; set; }
    public TimeSpan TotalTime { get; set; }
    public decimal Price { get; set; }
    [JsonIgnore]
    public EventType Type => EventType.VisitEnded;
}