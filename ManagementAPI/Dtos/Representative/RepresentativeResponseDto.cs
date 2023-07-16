using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using Shared.Constants;

namespace ManagementAPI.Dtos.Representative;

public class RepresentativeResponseDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = default!;
    public string IdentityNo { get; set; } = string.Empty;
    public short IdentityType { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public GeneralStatus Status { get; set; }
    public string CustomerName { get; set; } = default!;   
}
