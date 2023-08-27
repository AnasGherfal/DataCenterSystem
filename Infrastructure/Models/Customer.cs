using Infrastructure.Constants;
using Infrastructure.Events.Representative;

namespace Infrastructure.Models;

public class Customer: BaseModel

{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? SecondaryPhone { get; set; } 
    public string Email { get; set; } = string.Empty;
    public GeneralStatus Status { get; set; }

    //----------Relations
    public ICollection<Representative> Representatives { get; set; } =new List<Representative>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public ICollection<CustomerFile> Files { get; set; } = new List<CustomerFile>();

    
    
    
    public void Apply(RepresentativeCreatedEvent @event)
    {
        Representatives.Add(new Representative()
        {
            Id = @event.AggregateId,
            FirstName = @event.Data.FirstName,
            LastName = @event.Data.LastName,
            IdentityNo = @event.Data.IdentityNo,
            IdentityType = @event.Data.IdentityType,
            Email = @event.Data.Email,
            PhoneNo = @event.Data.PhoneNo,
            Status = GeneralStatus.Active,
            CreatedOn = @event.DateTime,
            
            // CreatedById = @event.UserId,
            Files = @event.Data.Documents.Select(p => new RepresentativeFile()
            {
                Id = p.FileIdentifier,
                FilePath = p.FileLink,
                DocType = p.FileType,
                IsActive = GeneralStatus.Active,
                CreatedOn = @event.DateTime,
            }).ToList()
        });
    }

}


