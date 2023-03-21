﻿namespace Infrastructure.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.UserName).IsRequired().HasMaxLength(50);
        builder.Property(p => p.FullName).HasMaxLength(100).IsRequired();
        builder.Property(p => p.EmpId).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        //builder.HasMany<Permission>().WithMany(p => p.Users);
        
        builder.HasOne(u => u.CreatedBy)
       .WithMany(u => u.UsersCreatedBy)
       .HasForeignKey(u => u.CreatedById)
       .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

