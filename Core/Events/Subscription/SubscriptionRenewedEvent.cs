using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Subscription;

public class SubscriptionRenewedEvent: EventStore<SubscriptionRenewedEventData>
{
    protected SubscriptionRenewedEvent() { }

    public SubscriptionRenewedEvent(Guid userId, Guid aggregateId, long sequence, SubscriptionRenewedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class SubscriptionRenewedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.SubscriptionRenewed;
}