using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Admin;

public class AdminLockedEvent: EventStore<AdminLockedEventData>
{
    protected AdminLockedEvent() { }

    public AdminLockedEvent(Guid userId, Guid aggregateId, long sequence, AdminLockedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class AdminLockedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.AdminLocked;
}