using Infrastructure.Constants;
using Infrastructure.Entities.Mappers;
using Infrastructure.Events.Invoice;

namespace Infrastructure.Entities;

public class Invoice: Entity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public decimal Price { get; set; }
    public string Notes { get; set; } = string.Empty;
    public InvoiceStatus Status { get; set; }
    public ICollection<VisitForInvoice> Visits { get; set; } = new List<VisitForInvoice>();
    public Customer? Customer { get; set; }
    
    
    public void Apply(InvoiceCreatedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        Id = @event.AggregateId;
        CustomerId = @event.Data.CustomerId;
        From = @event.Data.IncludeVisitsFrom;
        To = @event.Data.IncludeVisitsTo;
        Price = @event.Data.Price;
        Notes = @event.Data.Notes;
        Status = InvoiceStatus.Pending;
        Visits = @event.Data.VisitsId.Select(x => new VisitForInvoice()
        {
            Id = Guid.NewGuid(),
            InvoiceId = @event.AggregateId,
            VisitId = x,
        }).ToList();
    }
    
    
    public void Apply(InvoicePaidEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Status = InvoiceStatus.Paid;
    }
}