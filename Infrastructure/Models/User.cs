using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models; 

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public short Status { get; set; }
    public int EmpId { get; set; }
    public DateTime CreatedOn { get; set; }
    public int CreatedById { get; set; }

    //Realation
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();


}
