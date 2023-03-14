using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.EntityConfigurations;

public class UserPermissionConfig : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.HasKey(a => new {a.UserId,a.PermissionId});
      //  builder.HasOne<User>(s=>s.User).WithMany(a=>a.UserPermissions);
      //  builder.HasOne<Permission>(s => s.Permission).WithMany(a => a.UserPermissions);

        throw new NotImplementedException();
    }
}


