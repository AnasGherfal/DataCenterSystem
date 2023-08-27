using Infrastructure.Constants;

namespace Infrastructure.Entities.Mappers;

public class RepresentativeForVisit
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid? VisitId { get; set; } = Guid.Empty;
    public Guid? RepresentativeId { get; set; } = Guid.Empty;
    public Representative? Representative { get; set; }
}