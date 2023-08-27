using System.Text.Json.Serialization;
using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;

namespace Infrastructure.Events.Customer;

public class CustomerLockedEvent : EventStore<CustomerLockedEventData>
{
    protected CustomerLockedEvent() { }
    public CustomerLockedEvent(Guid userId, Guid aggregateId, long sequence, CustomerLockedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }

}
public class CustomerLockedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.CustomerLocked;
}
