using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Subscription;

public class SubscriptionFileUpdatedEvent: EventStore<SubscriptionFileUpdatedEventData>
{
    protected SubscriptionFileUpdatedEvent() { }

    public SubscriptionFileUpdatedEvent(Guid userId, Guid aggregateId, long sequence, SubscriptionFileUpdatedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class SubscriptionFileUpdatedEventData : IEventData
{
    public Guid FileIdentifier { get; set; } = Guid.Empty;
    public DocumentType FileType { get; set; } = DocumentType.Passport;
    public string FileLink { get; set; } = string.Empty;
    [JsonIgnore]
    public EventType Type => EventType.SubscriptionFileUpdated;
}