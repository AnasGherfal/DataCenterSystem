namespace Infrastructure.Models;

public abstract class BaseModel
{
    public DateTime CreatedOn { get; set; }
    public Guid? CreatedById { get; set; } = Guid.Parse("a9865faf-2339-4f39-12f4-08db85e40bb5");
    public User CreatedBy { get; set; } = default!;
}
