namespace Infrastructure.Models;

public class Customer: BaseModel

{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
   // public string? Address { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? SecondaryPhone { get; set; } 
    public string Email { get; set; } = string.Empty;
    public short Status { get; set; }

    //----------Relations
    public ICollection<Representive> Representives { get; set; } =new List<Representive>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public ICollection<CustomerFile> Files { get; set; } = new List<CustomerFile>();


}


