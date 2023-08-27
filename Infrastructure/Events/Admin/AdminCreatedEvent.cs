using Common.Constants;
using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Admin;

public class AdminCreatedEvent: EventStore<AdminCreatedEventData>
{
    protected AdminCreatedEvent() { }

    public AdminCreatedEvent(Guid userId, Guid aggregateId, AdminCreatedEventData data) : base(userId, aggregateId, 1, data)
    {
    }
}

public class AdminCreatedEventData : IEventData
{
    public string SecurityStamp { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public SystemPermissions Permissions { get; set; } = SystemPermissions.None;
    public int EmpId { get; set; } = 0;
    public string Password { get; set; } = string.Empty;
    [JsonIgnore]
    public EventType Type => EventType.AdminCreated;
}