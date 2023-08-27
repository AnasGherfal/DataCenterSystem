using Infrastructure.Constants;

namespace Infrastructure.Entities.Mappers;

public class CompanionForVisit
{
    public Guid Id { get; set; } = Guid.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentityNo { get; set; } = string.Empty;
    public IdentityType IdentityType { get; set; }
    public string JobTitle { get; set; } = string.Empty;
}