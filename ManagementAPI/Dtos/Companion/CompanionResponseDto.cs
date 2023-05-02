namespace ManagementAPI.Dtos.Companion;

public class CompanionResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string IdentityNo { get; set; } = String.Empty;
    public short IdentityType { get; set; }
    public string? JobTitle { get; set; } = String.Empty;
}
