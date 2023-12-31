using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Invoice;

public class InvoicePaidEvent: EventStore<InvoicePaidEventData>
{
    protected InvoicePaidEvent() { }

    public InvoicePaidEvent(Guid userId, Guid aggregateId, long sequence, InvoicePaidEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class InvoicePaidEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.InvoicePaid;
}