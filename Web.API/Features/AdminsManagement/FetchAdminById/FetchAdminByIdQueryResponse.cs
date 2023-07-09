using Common.Constants;

namespace Web.API.Features.AdminsManagement.FetchAdminById
{
    public class FetchAdminByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public SystemPermissions Permissions { get; set; } = SystemPermissions.None;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; }
    }
}
