namespace Infrastructure.Models;

public class RepresentativeVisit
{
    public int RepresentativeId { get; set; }
    public int VisitId { get; set; }
    public Representative Representative { get; set; }
    public Visit Visit { get; set; }
}
