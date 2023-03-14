namespace Infrastructure.Models;

public class UserPermission:BaseModel
{
    public int UserId { get; set; }
    public int PermissionId { get; set; }


    //Relation------------------
    public User User { get; set; } = new();
    public Permission Permission { get; set; } = new();
}
