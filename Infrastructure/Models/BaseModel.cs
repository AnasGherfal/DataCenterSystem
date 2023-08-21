namespace Infrastructure.Models;

public abstract class BaseModel
{
    public DateTime CreatedOn { get; set; }
    public Guid? CreatedById { get; set; } = Guid.Parse("c9dc9585-46bf-4a7c-f2f3-08db9bd865d4");
    public User CreatedBy { get; set; } = default!;
}
