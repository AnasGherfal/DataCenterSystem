using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Subscription;

public class SubscriptionRejectedEvent: EventStore<SubscriptionRejectedEventData>
{
    protected SubscriptionRejectedEvent() { }

    public SubscriptionRejectedEvent(Guid userId, Guid aggregateId, long sequence, SubscriptionRejectedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class SubscriptionRejectedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.SubscriptionRejected;
}