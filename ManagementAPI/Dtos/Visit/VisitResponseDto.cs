using ManagementAPI.Dtos.Companion;
<<<<<<< Updated upstream
using ManagementAPI.Dtos.Representive;
=======
using ManagementAPI.Dtos.Representative;
>>>>>>> Stashed changes
using Shared.Constants;

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
<<<<<<< Updated upstream
    public IList<RepresentiveResponseDto> Representives { get; set; }
=======
    public IList<RepresentativeResponseDto> Representatives { get; set; }
>>>>>>> Stashed changes
    public IList<CompanionResponseDto> Companions { get; set; }
    public string VisitType { get; set; }
    public int? InvoiceId { get; set; }
    
}
