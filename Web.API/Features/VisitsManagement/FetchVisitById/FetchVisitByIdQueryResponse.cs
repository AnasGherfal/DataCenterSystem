using Infrastructure.Constants;
using Shared.Constants;

namespace Web.API.Features.VisitsManagement.FetchVisitById
{
    public sealed record FetchVisitByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string Customer { get; set; } = string.Empty;
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
        public IList<FetchVisitByIdQueryResponseRepresentative> Representatives { get; set; } = new List<FetchVisitByIdQueryResponseRepresentative>();
        public IList<FetchVisitByIdQueryResponseCompanion> Companions { get; set; } = new List<FetchVisitByIdQueryResponseCompanion>();
    }
    
    public sealed record FetchVisitByIdQueryResponseRepresentative
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentityNo { get; set; } = string.Empty;
        public IdentityType IdentityType { get; set; }
    }
    
    public sealed record FetchVisitByIdQueryResponseCompanion
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentityNo { get; set; } = string.Empty;
        public IdentityType IdentityType { get; set; }
        public string JobTitle { get; set; } = string.Empty;
    }
}
