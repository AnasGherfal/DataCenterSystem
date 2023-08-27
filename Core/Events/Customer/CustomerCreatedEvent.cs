using System.Text.Json.Serialization;
using Core.Constants;
using Core.Events.Abstracts;

namespace Core.Events.Customer;

public class CustomerCreatedEvent : EventStore<CustomerCreatedEventData>
{
    protected CustomerCreatedEvent() { }

    public CustomerCreatedEvent(Guid userId, Guid aggregateId, CustomerCreatedEventData data) : base(userId, aggregateId, 1, data)
    {
    }
}
public class CustomerCreatedEventData : IEventData
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PrimaryPhone { get; set; } = string.Empty;
    public string SecondaryPhone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<FileStorageData> Documents { get; set; } = new();
    [JsonIgnore]
    public EventType Type => EventType.CustomerCreated;
}