namespace Infrastructure.Entities;

public abstract class Entity
{
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow; 
    public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    public long Sequence { get; set; } = 1;
    public byte[] RowVersion { get; set; } = { 1 };
}