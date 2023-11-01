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
    [JsonIgnore]
    public EventType Type => EventType.SubscriptionApproved;
}