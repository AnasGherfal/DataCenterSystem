using Core.Constants;
using Core.Events.Admin;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class Admin : IdentityUser<Guid>, IBaseEntity
{
    public string DisplayName { get; set; } = string.Empty;
    public int EmployeeId { get; set; } = 0;
    public SystemPermissions Permissions { get; set; } = SystemPermissions.None;
    public bool Enabled { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow; 
    public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    public long Sequence { get; set; } = 1;
    public byte[] RowVersion { get; set; } = { 1 };
    
    public void Apply(AdminCreatedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        Id = @event.AggregateId;
        Email = @event.Data.Email;
        DisplayName = @event.Data.FullName;
        EmployeeId = @event.Data.EmpId;
        Permissions = @event.Data.Permissions;
        UserName = Guid.NewGuid().ToString().Replace("-", "");
        EmailConfirmed = true;
        Enabled = true;
        SecurityStamp = Guid.NewGuid().ToString();
    }
    
    
    public void Apply(AdminLockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Enabled = false;
    }
    
    public void Apply(AdminUnlockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Enabled = true;
    }
    
    public void Apply(AdminUpdatedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Permissions = @event.Data.Permissions;
    }
    
}

public class AdminRole : IdentityRole<Guid>
{
}