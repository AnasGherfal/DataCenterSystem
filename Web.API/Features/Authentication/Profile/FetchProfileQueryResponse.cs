using Core.Constants;

namespace Web.API.Features.Authentication.Profile
{
    public sealed record FetchProfileQueryResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public SystemPermissions Permissions { get; set; } = SystemPermissions.None;
        public DateTime CreatedOn { get; set; }
    }
}
