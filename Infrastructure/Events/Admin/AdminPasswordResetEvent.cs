using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Admin;

public class AdminPasswordResetEvent: EventStore<AdminPasswordResetEventData>
{
    protected AdminPasswordResetEvent() { }

    public AdminPasswordResetEvent(Guid userId, Guid aggregateId, long sequence, AdminPasswordResetEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class AdminPasswordResetEventData : IEventData
{
    public string NewPassword { get; set; } = string.Empty;
    [JsonIgnore]
    public EventType Type => EventType.AdminPasswordReset;
}