using ManagementAPI.Dtos.Visit;
using Shared.Dtos;

namespace ManagementAPI.Dtos.Subscriptions
{
    public class SubscriptionRsponseDto
    {
        public Guid Id { get; set; }
        public int DaysRemaining { get; set; }
        public string ServiceName { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public short Status { get; set; }
        public decimal TotalPrice { get; set; }
        public int MonthlyVisits { get; set; }
        public FileResponseDto File { get; set; } = default!;
        public IList<VisitResponseDto> Visits { get; set; }= new List<VisitResponseDto>();
    }
}
