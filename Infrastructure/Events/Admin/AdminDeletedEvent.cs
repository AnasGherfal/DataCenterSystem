using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Admin;

public class AdminDeletedEvent: EventStore<AdminDeletedEventData>
{
    protected AdminDeletedEvent() { }

    public AdminDeletedEvent(Guid userId, Guid aggregateId, long sequence, AdminDeletedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class AdminDeletedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.AdminDeleted;
}