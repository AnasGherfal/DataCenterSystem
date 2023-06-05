using Infrastructure.Constants;
using Infrastructure.Models;
<<<<<<< Updated upstream
using ManagementAPI.Dtos.Representive;
=======
using ManagementAPI.Dtos.Representative;
>>>>>>> Stashed changes
using Shared.Constants;

namespace ManagementAPI.Dtos.Customer;

public class CustomerResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? SecondaryPhone { get; set; }
    public string Email { get; set; } = string.Empty;
    public GeneralStatus Status { get; set; }
<<<<<<< Updated upstream
=======
    public  IList<int> Subsicrptions { get; set; }
    public IList<int> Representative { get; set; }
>>>>>>> Stashed changes
    public IList<string> FileName { get; set; }
}