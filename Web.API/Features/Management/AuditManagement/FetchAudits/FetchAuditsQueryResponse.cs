namespace Web.API.Features.Management.AuditManagement.FetchAudits
{
    public sealed record FetchAuditsQueryResponse
    {
        public string Type { get; set; } = string.Empty;
        public DateTime OccuredOn { get; set; } = DateTime.MinValue;
        public Guid UserId { get; set; } = Guid.Empty;
        public string UserName { get; set; }= string.Empty;
    }
}
