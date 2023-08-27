namespace Web.API.Features.AdminsManagement.FetchPermissions
{
    public record FetchPermissionsQueryResponse
    {
        public long Id { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
    }
}
