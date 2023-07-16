namespace Infrastructure.Models;

public class Companion : BaseModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string FullName { get; set; } = string.Empty;
    public string IdentityNo { get; set; } = String.Empty;
    public short IdentityType { get; set; } 
    //
    public string? JobTitle { get; set; } = String.Empty;
    public Guid VisitId { get; set; }

    //-----------Relations
    public Visit Visit { get; set; } = default!;
}
