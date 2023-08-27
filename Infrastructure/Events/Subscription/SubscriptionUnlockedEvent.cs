using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Subscription;

public class SubscriptionUnlockedEvent: EventStore<SubscriptionUnlockedEventData>
{
    protected SubscriptionUnlockedEvent() { }

    public SubscriptionUnlockedEvent(Guid userId, Guid aggregateId, long sequence, SubscriptionUnlockedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class SubscriptionUnlockedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.SubscriptionUnlocked;
}