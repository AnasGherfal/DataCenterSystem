namespace ManagementAPI.Dtos.Representive;

public class CreateVisitRequestDto
{
    public DateTime? ExpectedStartTime { get; set; }
    public DateTime? ExpectedEndTime { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? TotalMin { get; set; }
    public decimal Price { get; set; }
    public string? Notes { get; set; }
    //public short Status { get; set; }
    public short VisitTypeId { get; set; }
    //public int RepresentiveId { get; set; }
    public int VisitShiftId { get; set; }
    public int? InvoiceId { get; set; }
}
