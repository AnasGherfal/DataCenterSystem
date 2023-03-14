namespace Infrastructure.Models;

public class User : BaseModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int EmpId { get; set; }
    public short Status { get; set; }

    //---------realations
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    //public ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
}
