using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models;

public class Permission 
{
    public int Id { get; set; }
    public string Name { get; set; }=string.Empty;
    public int EntityId { get; set; }

    //--------Relations
    //note changes
    //--
    
    public ICollection<User> Users { get; set; } = new List<User>();
    public Entity Entity { get; set; } = new();
}
