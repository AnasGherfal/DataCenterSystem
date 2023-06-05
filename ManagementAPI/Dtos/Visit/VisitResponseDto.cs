

using ManagementAPI.Dtos.Companion;
using ManagementAPI.Dtos.Representative;

namespace ManagementAPI.Dtos.Visit;
public class VisitResponseDto
{
    public int Id { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? TotalMin { get; set; }
    public decimal Price { get; set; }
    public string? Notes { get; set; }
    public string TimeShift { get; set; }
    public string  CustomerName { get; set; }
    public IList<RepresentativeResponseDto> Representatives { get; set; }
    public IList<CompanionResponseDto> Companions { get; set; }
    public string VisitType { get; set; }
    public int? InvoiceId { get; set; }
    
}
