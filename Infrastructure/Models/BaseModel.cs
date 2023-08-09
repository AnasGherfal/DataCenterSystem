namespace Infrastructure.Models;

public abstract class BaseModel
{
    public DateTime CreatedOn { get; set; }
    public Guid? CreatedById { get; set; } = Guid.Parse("10cbc37c-bacf-491a-cade-08db971c0334");
    public User CreatedBy { get; set; } = default!;
}
