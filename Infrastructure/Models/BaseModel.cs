namespace Infrastructure.Models;

public abstract class BaseModel
{
    public DateTime CreatedOn { get; set; }
    public Guid? CreatedById { get; set; } = Guid.Parse("d8c52661-e8b1-4c18-2c70-08db9347ddda");
    public User CreatedBy { get; set; } = default!;
}
