using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Representative;

public class RepresentativeDeletedEvent: EventStore<RepresentativeDeletedEventData>
{
    protected RepresentativeDeletedEvent() { }

    public RepresentativeDeletedEvent(Guid userId, Guid aggregateId, long sequence, RepresentativeDeletedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class RepresentativeDeletedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.RepresentativeDeleted;
}