using Infrastructure.Constants;
using ManagementAPI.Dtos.Visit;

namespace ManagementAPI.Dtos.Invoice;

public class InvoiceResponseDto
   {
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Description { get; set; }
    public string InvoiceNo { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public GeneralStatus Status { get; set; }
    public bool IsPaid { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public Guid SubscriptionId { get; set; }
    public IList<VisitResponseDto> Visits { get; set; }=new List<VisitResponseDto>();
}
//DateTime Date, bool IsPayed, short Status, decimal TotalPrice, string CustomerName, int SubsicrptionId