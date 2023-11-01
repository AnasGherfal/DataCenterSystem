using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Representative;

public class RepresentativeRejectedEvent: EventStore<RepresentativeRejectedEventData>
{
    protected RepresentativeRejectedEvent() { }

    public RepresentativeRejectedEvent(Guid userId, Guid aggregateId, long sequence, RepresentativeRejectedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class RepresentativeRejectedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.RepresentativeRejected;
}