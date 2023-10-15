namespace Web.API.Features.VisitTypesManagement.FetchVisitTypes
{
    public record FetchVisitTypesQueryResponse
    {
        public long Id { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
    }
}
