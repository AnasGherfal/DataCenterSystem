using Infrastructure.Constants;

namespace ManagementAPI.Dtos.Customer;

public class CustomerResponseDto {

    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Address { get; set; }
    public string PrimaryPhone { get; set; } = default!;
    public string? SecondaryPhone { get; set; }
    public string Email { get; set; } = default!;
    public GeneralStatus Status { get; set; }

    public IList<int> Subsicrptions { get; set; } = new List<int>();
    public IList<int> Representative { get; set; }= new List<int>();
    public IList<string> FileName { get; set; } = new List<string>();
   }