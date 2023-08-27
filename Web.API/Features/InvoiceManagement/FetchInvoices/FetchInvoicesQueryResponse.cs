using Core.Constants;

namespace Web.API.Features.InvoiceManagement.FetchInvoices
{
    public sealed record FetchInvoicesQueryResponse
    {
        public Guid Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public DateTime VisitsFrom { get; set; }
        public DateTime VisitsTo { get; set; }
        public decimal Price { get; set; }
        public InvoiceStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
