using Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public class RepresentiveFileConfig : IEntityTypeConfiguration<RepresentiveFile>
{
    public void Configure(EntityTypeBuilder<RepresentiveFile> builder)
    {
         builder.HasKey(a => a.Id);
         builder.Property(p=>p.Id).ValueGeneratedNever();    

    }
}
