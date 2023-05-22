namespace Infrastructure.Configurations;

public class RepresentiveVisitConfig : IEntityTypeConfiguration<RepresentiveVisit>
{
    public void Configure(EntityTypeBuilder<RepresentiveVisit> builder)
    {
        builder.HasKey(p => new {
            p.RepresentiveId,
            p.VisitId
        });
        builder.HasOne(p => p.Visit)
               .WithMany(p => p.RepresentivesVisits)
               .HasForeignKey(p => p.VisitId)
               .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(p => p.Representive)
               .WithMany(p => p.RepresentiveVisits)
               .HasForeignKey(p => p.RepresentiveId)
               .OnDelete(DeleteBehavior.ClientSetNull);
    }
}