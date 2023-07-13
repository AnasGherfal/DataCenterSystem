using Infrastructure.Constants;

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


}


