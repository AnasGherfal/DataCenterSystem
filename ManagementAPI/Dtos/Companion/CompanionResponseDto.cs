namespace ManagementAPI.Dtos.Companion;

public class CompanionResponseDto
{
   public Guid Id { set; get; }
   public string FirstName { set; get; } = string.Empty;
   public string LastName { set; get; } = string.Empty;
   public string FullName { set; get; } = string.Empty;
   public string JobTitle { set; get; } = string.Empty;
   public string IdentityNo { set; get; } = string.Empty;
   public short IdentityType { set; get; } 

}

