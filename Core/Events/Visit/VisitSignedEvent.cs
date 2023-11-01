using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Visit;

public class VisitSignedEvent: EventStore<VisitSignedEventData>
{
    protected VisitSignedEvent() { }

    public VisitSignedEvent(Guid userId, Guid aggregateId, long sequence, VisitSignedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class VisitSignedEventData : IEventData
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan TotalTime { get; set; }
    public decimal Price { get; set; }
    [JsonIgnore]
    public EventType Type => EventType.VisitSigned;
}