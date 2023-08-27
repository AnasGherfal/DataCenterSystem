using System.Text.Json.Serialization;
using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;

namespace Infrastructure.Events.Customer;

public class CustomerDeletedEvent : EventStore<CustomerDeletedEventData>
{
    protected CustomerDeletedEvent() { }
    public CustomerDeletedEvent(Guid userId, Guid aggregateId, long sequence, CustomerDeletedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}
public class CustomerDeletedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.CustomerDeleted;
}