namespace Infrastructure.Models;

public abstract class BaseModel
{
    public DateTime CreatedOn { get; set; }
    public Guid? CreatedById { get; set; } = Guid.Parse("ffb30b3b-0740-4079-689a-08db91bdd34c");
    public User CreatedBy { get; set; } = default!;
}
