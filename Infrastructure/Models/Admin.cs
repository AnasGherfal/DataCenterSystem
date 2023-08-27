using Common.Constants;
using Infrastructure.Constants;
using Infrastructure.Events.Admin;
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
    
    public void Apply(AdminCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Email = @event.Data.Email;
        DisplayName = @event.Data.FullName;
        EmployeeId = @event.Data.EmpId;
        Permissions = @event.Data.Permissions;
        UserName = Guid.NewGuid().ToString().Replace("-", "");
        EmailConfirmed = true;
        Enabled = true;
        SecurityStamp = Guid.NewGuid().ToString();
        CreatedOn = @event.DateTime;
        // CreatedById = @event.UserId,
    }
}

public class AdminRole : IdentityRole<Guid>
{
}