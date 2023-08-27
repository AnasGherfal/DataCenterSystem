using Infrastructure.Models;
namespace Infrastructure.Configurations;

public class RepresentativeVisitConfig : IEntityTypeConfiguration<RepresentativeVisit>
{
    public void Configure(EntityTypeBuilder<RepresentativeVisit> builder)
    {
        builder.HasKey(p => new {
            p.RepresentativeId,
            p.VisitId
        });
        builder.HasOne(p => p.Visit)
               .WithMany(p => p.RepresentativesVisits)
               .HasForeignKey(p => p.VisitId)
               .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(p => p.Representative)
               .WithMany(p => p.RepresentativeVisits)
               .HasForeignKey(p => p.RepresentativeId)
               .OnDelete(DeleteBehavior.ClientSetNull);
    }
}