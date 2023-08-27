using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Customer;

public class CustomerFileUpdatedEvent: EventStore<CustomerFileUpdatedEventData>
{
    protected CustomerFileUpdatedEvent() { }

    public CustomerFileUpdatedEvent(Guid userId, Guid aggregateId, long sequence, CustomerFileUpdatedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class CustomerFileUpdatedEventData : IEventData
{
    public Guid? OldFileIdentifier { get; set; }
    public Guid FileIdentifier { get; set; } = Guid.Empty;
    public DocumentType FileType { get; set; } = DocumentType.Passport;
    public string FileLink { get; set; } = string.Empty;
    [JsonIgnore]
    public EventType Type => EventType.CustomerFileUpdated;
}