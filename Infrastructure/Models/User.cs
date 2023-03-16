using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models; 

public class User : BaseModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public short Status { get; set; }
    public int EmpId { get; set; }

    //Realation
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public ICollection<User> UsersCreatedBy { get; set; } = new List<User>();

}
