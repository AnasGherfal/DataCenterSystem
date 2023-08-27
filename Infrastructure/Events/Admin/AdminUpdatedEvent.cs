using Common.Constants;
using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Admin;

public class AdminUpdatedEvent: EventStore<AdminUpdatedEventData>
{
    protected AdminUpdatedEvent() { }

    public AdminUpdatedEvent(Guid userId, Guid aggregateId, long sequence, AdminUpdatedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class AdminUpdatedEventData : IEventData
{
    public SystemPermissions Permissions { get; set; } = SystemPermissions.None;
    [JsonIgnore]
    public EventType Type => EventType.AdminUpdated;
}