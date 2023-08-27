using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Subscription;

public class SubscriptionDeletedEvent: EventStore<SubscriptionDeletedEventData>
{
    protected SubscriptionDeletedEvent() { }

    public SubscriptionDeletedEvent(Guid userId, Guid aggregateId, long sequence, SubscriptionDeletedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class SubscriptionDeletedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.SubscriptionDeleted;
}