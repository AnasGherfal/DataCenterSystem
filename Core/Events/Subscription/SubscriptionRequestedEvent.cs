using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Subscription;

public class SubscriptionRequestedEvent: EventStore<SubscriptionRequestedEventData>
{
    protected SubscriptionRequestedEvent() { }

    public SubscriptionRequestedEvent(Guid userId, Guid aggregateId, SubscriptionRequestedEventData data) : base(userId, aggregateId, 1, data)
    {
    }
}

public class SubscriptionRequestedEventData : IEventData
{
    public Guid ServiceId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public FileStorageData Documents { get; set; } = new();
    [JsonIgnore]
    public EventType Type => EventType.SubscriptionRequested;
}