using Core.Constants;
using Core.Events.Admin;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class Admin : Entity
{
    public Guid Id { get; set; }
    public int EmployeeId { get; set; } = 0;
    
    public void Apply(AdminCreatedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        EmployeeId = @event.Data.EmpId;
    }
    
    public void Apply(AdminLockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
    }
    
    public void Apply(AdminUnlockedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
    }
    
    public void Apply(AdminUpdatedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
    }
}