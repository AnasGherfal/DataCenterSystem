using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Admin;

public class AdminPasswordChangedEvent: EventStore<AdminPasswordChangedEventData>
{
    protected AdminPasswordChangedEvent() { }

    public AdminPasswordChangedEvent(Guid userId, Guid aggregateId, long sequence, AdminPasswordChangedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class AdminPasswordChangedEventData : IEventData
{
    public string NewPassword { get; set; } = string.Empty;
    [JsonIgnore]
    public EventType Type => EventType.AdminPasswordChanged;
}