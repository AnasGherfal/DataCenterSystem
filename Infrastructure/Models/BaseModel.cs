using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models;

public abstract class BaseModel
{
    public DateTime CreatedOn { get; set; }
    
    public int CreatedById { get; set; }

    //------Relation
  //  [NotMapped]
   public User CreatedBy { get; set; } = new User();

}
