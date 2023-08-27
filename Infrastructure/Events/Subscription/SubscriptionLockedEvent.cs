using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Subscription;

public class SubscriptionLockedEvent: EventStore<SubscriptionLockedEventData>
{
    protected SubscriptionLockedEvent() { }

    public SubscriptionLockedEvent(Guid userId, Guid aggregateId, long sequence, SubscriptionLockedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class SubscriptionLockedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.SubscriptionLocked;
}