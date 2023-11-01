using Core.Constants;

namespace Web.API.Features.Consumer.VisitsManagement.FetchMyVisitById
{
    public sealed record FetchMyVisitByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string Service { get; set; } = string.Empty;
        public DateTime ExpectedStartTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan? TotalTime { get; set; }
        public decimal VisitPrice { get; set; }
        public string Notes { get; set; } = string.Empty;
        public VisitType VisitType { get; set; } = VisitType.Other;
        public bool IsPaid { get; set; }
        public DateTime CreatedOn { get; set; }
        public IList<FetchMyVisitByIdQueryResponseRepresentative> Representatives { get; set; } = new List<FetchMyVisitByIdQueryResponseRepresentative>();
        public IList<FetchMyVisitByIdQueryResponseCompanion> Companions { get; set; } = new List<FetchMyVisitByIdQueryResponseCompanion>();
    }
    
    public sealed record FetchMyVisitByIdQueryResponseRepresentative
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentityNo { get; set; } = string.Empty;
        public IdentityType IdentityType { get; set; }
    }
    
    public sealed record FetchMyVisitByIdQueryResponseCompanion
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentityNo { get; set; } = string.Empty;
        public IdentityType IdentityType { get; set; }
        public string JobTitle { get; set; } = string.Empty;
    }
}
