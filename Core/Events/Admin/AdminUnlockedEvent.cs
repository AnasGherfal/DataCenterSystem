using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Admin;

public class AdminUnlockedEvent: EventStore<AdminUnlockedEventData>
{
    protected AdminUnlockedEvent() { }

    public AdminUnlockedEvent(Guid userId, Guid aggregateId, long sequence, AdminUnlockedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class AdminUnlockedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.AdminUnlocked;
}