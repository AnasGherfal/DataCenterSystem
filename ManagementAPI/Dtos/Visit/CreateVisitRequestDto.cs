using ManagementAPI.Dtos.Companion;
namespace ManagementAPI.Dtos.Visit;
public class CreateVisitRequestDto
{
    public DateTime? ExpectedStartTime { get; set; }
    public DateTime? ExpectedEndTime { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Notes { get; set; }
    public string VisitTypeId { get; set; }
    public string SubscriptionId { get; set; }
    public IList<CreateCompanionRequestDto> Companions { get; set; } = new List<CreateCompanionRequestDto>();
    public IList<Guid> Representatives { get; set; } = new List<Guid>();
}
