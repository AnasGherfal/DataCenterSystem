using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.EntityConfigurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.UserName).IsRequired().HasMaxLength(50);
        builder.Property(p => p.FullName).HasMaxLength(100).IsRequired();
        builder.Property(p => p.EmpId).IsRequired();
        builder.Property(p => p.Status).IsRequired();
      //  builder.HasMany<Permission>().WithMany(p => p.Users);
        //builder.HasOne(u => u.CreatedBy)
            //  .WithMany(u => u.UsersCreatedBy)
              //.OnDelete(DeleteBehavior.SetNull);
    }
}

