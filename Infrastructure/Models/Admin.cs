using Common.Constants;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models;

public class Admin : IdentityUser<Guid>, IBaseEntity
{
    public string DisplayName { get; set; } = string.Empty;
    public int EmployeeId { get; set; } = 0;
    public SystemPermissions Permissions { get; set; } = SystemPermissions.None;
    public bool Enabled { get; set; } = true;
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}

public class AdminRole : IdentityRole<Guid>
{
}