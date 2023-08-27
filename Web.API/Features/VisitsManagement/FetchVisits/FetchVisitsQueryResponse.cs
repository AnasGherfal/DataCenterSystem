using Infrastructure.Constants;

namespace Web.API.Features.VisitsManagement.FetchVisits
{
    public sealed record FetchVisitsQueryResponse
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime ExpectedStartTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public TimeSpan? TotalMinutes { get; set; }
        public decimal Price { get; set; }
        public string Notes { get; set; } = string.Empty;
        public VisitType VisitType { get; set; } = VisitType.Other;
        public DateTime CreatedOn { get; set; }
    }
}
