using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Representative;

public class RepresentativeUnlockedEvent: EventStore<RepresentativeUnlockedEventData>
{
    protected RepresentativeUnlockedEvent() { }

    public RepresentativeUnlockedEvent(Guid userId, Guid aggregateId, long sequence, RepresentativeUnlockedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class RepresentativeUnlockedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.RepresentativeUnlocked;
}