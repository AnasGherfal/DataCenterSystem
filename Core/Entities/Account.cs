using Core.Constants;
using Core.Entities.Mappers;
using Core.Events.Admin;
using Core.Events.Customer;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class AccountRole : IdentityRole<Guid> {  }
public class Account : IdentityUser<Guid>, IBaseEntity
{
    public AccountType AccountType { get; set; } = AccountType.Customer;
    public Customer? Customer { get; set; }
    public Admin? Admin { get; set; }
    public string DisplayName { get; set; } = string.Empty;
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
        UserName = @event.Data.Email;
        Permissions = @event.Data.Permissions;
        EmailConfirmed = true;
        Enabled = true;
        SecurityStamp = Guid.NewGuid().ToString();
        AccountType = AccountType.Admin;
        Admin = new Admin();
        Admin.Apply(@event);
    }
    
    public void Apply(AdminLockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Enabled = false;
        Admin?.Apply(@event);
    }
    
    public void Apply(AdminUnlockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Enabled = true;
        Admin?.Apply(@event);
    }
    
    public void Apply(AdminUpdatedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Permissions = @event.Data.Permissions;
        Admin?.Apply(@event);
    }
    
    //Customer
    public void Apply(CustomerCreatedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        Id = @event.AggregateId;
        Email = @event.Data.Email;
        UserName = @event.Data.Email;
        Customer = new Customer();
        Customer.Apply(@event);
    }
    public void Apply(CustomerUpdatedEvent @event)
    {
        Email = @event.Data.Email;
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Customer?.Apply(@event);
    }

    public void Apply(CustomerLockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Enabled = false;
        Customer?.Apply(@event);
    }
    
    public void Apply(CustomerUnlockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Enabled = true;
        Customer?.Apply(@event);
    }
    
    public void Apply(CustomerDeletedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        IsDeleted = true;
        Customer?.Apply(@event);
    }
    
    public void Apply(CustomerFileUpdatedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        Customer?.Apply(@event);
    }
}
