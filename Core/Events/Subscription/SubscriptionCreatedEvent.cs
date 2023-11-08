using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Subscription;

public class SubscriptionCreatedEvent: EventStore<SubscriptionCreatedEventData>
{
    protected SubscriptionCreatedEvent() { }

    public SubscriptionCreatedEvent(Guid userId, Guid aggregateId, SubscriptionCreatedEventData data) : base(userId, aggregateId, 1, data)
    {
    }
}

public class SubscriptionCreatedEventData : IEventData
{
    public Guid ServiceId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime ContractDate { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public FileStorageData Documents { get; set; } = new();
    [JsonIgnore]
    public EventType Type => EventType.SubscriptionCreated;
}