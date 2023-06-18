
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
    public string TimeShift { get; set; } = default!;
    public string CustomerName { get; set; } = default!;
    public IList<RepresentativeResponseDto> Representatives { get; set; } = new List<RepresentativeResponseDto>();
    public IList<CompanionResponseDto> Companions { get; set; } = new List<CompanionResponseDto>();
    public string VisitType { get; set; } = default!;
    public int? InvoiceId { get; set; }
    
}
