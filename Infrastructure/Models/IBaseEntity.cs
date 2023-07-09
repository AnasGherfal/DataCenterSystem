namespace Infrastructure.Models;

public interface IBaseEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}