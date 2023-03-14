using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.EntityConfigurations;

public class RepresentiveVisitConfig : IEntityTypeConfiguration<RepresentiveVisit>
{
    public void Configure(EntityTypeBuilder<RepresentiveVisit> builder)
    {
        builder.HasKey(p => new { p.RepresentiveId, p.VisitId });
}

}