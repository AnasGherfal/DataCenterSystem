namespace Infrastructure.Configurations;

public class CustomerFileConfig : IEntityTypeConfiguration<CustomerFile>
{
    public void Configure(EntityTypeBuilder<CustomerFile> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.CreatedBy)
                .WithMany(u => u.CustomerFilesCreatedBy)
                .HasForeignKey(a => a.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}