namespace Infrastructure.Models;

public class RepresentativeVisit
{
    public Guid RepresentativeId { get; set; }
    public Guid VisitId { get; set; }
    public Representative Representative { get; set; } = default!;
    public Visit Visit { get; set; } = default!;
}
