namespace Infrastructure.Models;

public class RepresentiveVisit
{
    public int RepresentiveId { get; set; }
    public int VisitId { get; set; }
    public Representive Representive { get; set; }
    public Visit Visit { get; set; }
}
