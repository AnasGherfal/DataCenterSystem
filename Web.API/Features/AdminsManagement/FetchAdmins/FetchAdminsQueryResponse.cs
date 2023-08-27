using Common.Constants;
using Infrastructure.Constants;

namespace Web.API.Features.AdminsManagement.FetchAdmins
{
    public sealed record FetchAdminsQueryResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public SystemPermissions Permissions { get; set; } = SystemPermissions.None;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; }
    }
}
