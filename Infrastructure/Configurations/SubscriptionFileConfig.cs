﻿namespace Infrastructure.Configurations;

public class SubscriptionFileConfig : IEntityTypeConfiguration<SubscriptionFile>
{
    public void Configure(EntityTypeBuilder<SubscriptionFile> builder)
    {
        builder.HasOne(a => a.CreatedBy)
                .WithMany(u => u.SubscriptionFilesCreatedBy)
                .HasForeignKey(a => a.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

