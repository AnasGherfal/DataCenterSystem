using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Representative;

public class RepresentativeLockedEvent: EventStore<RepresentativeLockedEventData>
{
    protected RepresentativeLockedEvent() { }

    public RepresentativeLockedEvent(Guid userId, Guid aggregateId, long sequence, RepresentativeLockedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class RepresentativeLockedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.RepresentativeLocked;
}