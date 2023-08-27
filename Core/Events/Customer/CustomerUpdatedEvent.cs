using System.Text.Json.Serialization;
using Core.Constants;
using Core.Events.Abstracts;
using Core.Interfaces.Dtos;

namespace Core.Events.Customer;

public class CustomerUpdatedEvent : EventStore<CustomerUpdatedEventData>
{
    protected CustomerUpdatedEvent() { }
    public CustomerUpdatedEvent(Guid userId, Guid aggregateId, long sequence, CustomerUpdatedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }

}
public class CustomerUpdatedEventData : IEventData
{
    
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? SecondaryPhone { get; set; }
    public string Email { get; set; } =string.Empty;
    public IList<FileStorageData> Files { get; set; }=new List<FileStorageData>();
    [JsonIgnore]
    public EventType Type => EventType.CustomerUpdated;
}
