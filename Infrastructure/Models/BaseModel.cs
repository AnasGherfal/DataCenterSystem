namespace Infrastructure.Models;

public abstract class BaseModel
{
    public DateTime CreatedOn { get; set; }
    public Guid? CreatedById { get; set; } = Guid.Parse("891690d5-1f72-48ce-ace9-08dba9486e55");
    public User CreatedBy { get; set; } = default!;
}
