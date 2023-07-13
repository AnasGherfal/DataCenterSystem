namespace Infrastructure.Models;

public abstract class BaseModel
{
    public DateTime CreatedOn { get; set; }
    public Guid? CreatedById { get; set; }
    public User CreatedBy { get; set; } = default!;
}
