namespace Infrastructure.Entities.Mappers;

public class VisitForInvoice
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid InvoiceId { get; set; } = Guid.Empty;
    public Guid? VisitId { get; set; } = Guid.Empty;
    public Visit? Visit { get; set; }
}