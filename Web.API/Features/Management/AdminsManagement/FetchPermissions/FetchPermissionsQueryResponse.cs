namespace Web.API.Features.Management.AdminsManagement.FetchPermissions
{
    public record FetchPermissionsQueryResponse
    {
        public long Id { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
    }
}
