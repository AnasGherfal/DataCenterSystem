namespace LTTDataCenter.DTOs.Requests.Create
{
    public class CreateVisit
    {
        public DateTime? ExpectedStartTime { get; set; }
        public DateTime? ExpectedEndTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Notes { get; set; }
        
       
    }
}
