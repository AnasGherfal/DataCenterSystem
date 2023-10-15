using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public abstract class Account : IdentityUser<Guid>, IBaseEntity
{
    public bool Enabled { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow; 
    public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    public long Sequence { get; set; } = 1;
    public byte[] RowVersion { get; set; } = { 1 };
}

public class AdminRole : IdentityRole<Guid>
{
}
