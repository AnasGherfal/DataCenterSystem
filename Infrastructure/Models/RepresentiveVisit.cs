namespace Infrastructure.Models
{
    public class RepresentiveVisit
    {
        public int RepresentiveId { get; set; }
        public int VisitId { get; set; }
        //---Relation----------------

        public Representive Representive { get; set; } = new();
        public Visit Visit { get; set; } = new();
    }
}
