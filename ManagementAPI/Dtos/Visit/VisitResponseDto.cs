using Shared.Constants;

namespace ManagementAPI.Dtos.Visit;

public class VisitResponseDto
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? TotalMin { get; set; }
    public decimal Price { get; set; }
    public string? Notes { get; set; }
    public string VisitShift { get; set; }
    public string  CustomerName { get; set; }
    public IList<string> RepresentivesNames { get; set; }
    public IList<string> CompanionsNames { get; set; }
    public string VisitType { get; set; }
    public int? InvoiceId { get; set; }
    
}
