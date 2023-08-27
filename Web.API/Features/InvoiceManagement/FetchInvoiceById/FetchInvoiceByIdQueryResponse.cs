using Core.Constants;

namespace Web.API.Features.InvoiceManagement.FetchInvoiceById
{
    public sealed record FetchInvoiceByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public DateTime VisitsFrom { get; set; }
        public DateTime VisitsTo { get; set; }
        public decimal Price { get; set; }
        public int NumberOfVisits { get; set; }
        public string Notes { get; set; } = string.Empty;
        public InvoiceStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
