using Core.Constants;

namespace Web.API.Features.AuditManagement.FetchProjectionById
{
    public sealed record FetchProjectionByIdQueryResponse
    {
        public EntityType Projection { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Before { get; set; } = string.Empty;
        public string After { get; set; } = string.Empty;
        public DateTime OccurredOn { get; set; }
        public string OccuredBy { get; set; } = string.Empty;
    }
}
