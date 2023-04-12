namespace ManagementAPI.Dtos.Visit;

public class CreateVisitRequestDto
{
    public DateTime? ExpectedStartTime { get; set; }
    public DateTime? ExpectedEndTime { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan? TotalMin { get; set; }
    public string? Notes { get; set; }
    public short VisitTypeId { get; set; }
    public int TimeShiftId { get; set; }
    public int? InvoiceId { get; set; }
    public int SubscriptionId { get; set; }
}
