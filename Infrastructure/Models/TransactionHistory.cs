namespace Infrastructure.Models;

public class TransactionHistory : BaseModel
{
    public int Id { get; set; }
    public short Action { get; set; }
    // public string EntityId { get; set; } = string.Empty;
    public short EntityType { get; set; }
    public string EntityData { get; set; }=string.Empty;

    //Relation-----------------------

    //public ICollection<User> Users { get; set; } = new List<User>();
   //public User MyProperty { get; set; }
}
