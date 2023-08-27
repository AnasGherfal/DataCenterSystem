using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Invoice;

public class InvoiceCreatedEvent: EventStore<InvoiceCreatedEventData>
{
    protected InvoiceCreatedEvent() { }

    public InvoiceCreatedEvent(Guid userId, Guid aggregateId, InvoiceCreatedEventData data) : base(userId, aggregateId, 1, data)
    {
    }
}

public class InvoiceCreatedEventData : IEventData
{
    public Guid CustomerId { get; set; }
    public DateTime IncludeVisitsFrom { get; set; }
    public DateTime IncludeVisitsTo { get; set; }
    public decimal Price { get; set; }
    public IList<Guid> VisitsId { get; set; } = new List<Guid>();
    public string Notes { get; set; } = string.Empty;
    [JsonIgnore]
    public EventType Type => EventType.InvoiceCreated;
}