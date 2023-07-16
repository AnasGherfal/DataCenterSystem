namespace ManagementAPI.Dtos.Companion;

public record CompanionResponseDto(Guid id,string FirstName, string LastName, string FullName, string JobTitle, string IdentityNo, short IdentityType);

