using System.Text.Json.Serialization;
using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;

namespace Infrastructure.Events.Customer;

public class CustomerUnlockedEvent : EventStore<CustomerUnlockedEventData>
{
    protected CustomerUnlockedEvent() { }
    public CustomerUnlockedEvent(Guid userId, Guid aggregateId, long sequence, CustomerUnlockedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }

}
public class CustomerUnlockedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.CustomerUnlocked;
}
