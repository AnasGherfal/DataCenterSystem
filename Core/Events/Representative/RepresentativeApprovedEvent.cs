using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Representative;

public class RepresentativeApprovedEvent: EventStore<RepresentativeApprovedEventData>
{
    protected RepresentativeApprovedEvent() { }

    public RepresentativeApprovedEvent(Guid userId, Guid aggregateId, long sequence, RepresentativeApprovedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class RepresentativeApprovedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.RepresentativeApproved;
}