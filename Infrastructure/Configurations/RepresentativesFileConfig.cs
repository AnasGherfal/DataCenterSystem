using Infrastructure.Models;
namespace Infrastructure.Configurations;

public class RepresentativeFileConfig : IEntityTypeConfiguration<RepresentativeFile>
{
    public void Configure(EntityTypeBuilder<RepresentativeFile> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.CreatedBy)
               .WithMany(u => u.RepresentativeFilesCreatedBy)
               .HasForeignKey(a => a.CreatedById)
               .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
