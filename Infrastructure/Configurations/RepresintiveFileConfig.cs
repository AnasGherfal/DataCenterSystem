namespace Infrastructure.Configurations;

public class RepresentiveFileConfig : IEntityTypeConfiguration<RepresentiveFile>
{
    public void Configure(EntityTypeBuilder<RepresentiveFile> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.CreatedBy)
               .WithMany(u => u.RepresentiveFilesCreatedBy)
               .HasForeignKey(a => a.CreatedById)
               .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
