namespace Infrastructure.Models;

public abstract class BaseModel
{
    public DateTime CreatedOn { get; set; }
    public Guid? CreatedById { get; set; } = Guid.Parse("b6f745f0-1778-434c-a1bd-08dba7b058fc");
    public User CreatedBy { get; set; } = default!;
}
