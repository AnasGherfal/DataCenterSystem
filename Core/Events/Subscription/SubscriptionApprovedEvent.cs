using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Subscription;

public class SubscriptionApprovedEvent: EventStore<SubscriptionApprovedEventData>
{
    protected SubscriptionApprovedEvent() { }

    public SubscriptionApprovedEvent(Guid userId, Guid aggregateId, long sequence, SubscriptionApprovedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class SubscriptionApprovedEventData : IEventData
{
    public DateTime ContractDate { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    [JsonIgnore]
    public EventType Type => EventType.SubscriptionApproved;
}