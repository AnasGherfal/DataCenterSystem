using Infrastructure.Constants;
using Shared.Dtos;

namespace ManagementAPI.Dtos.Customer;

public class CustomerResponseDto {

    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Address { get; set; }
    public string PrimaryPhone { get; set; } = default!;
    public string? SecondaryPhone { get; set; }
    public string Email { get; set; } = default!;
    public GeneralStatus Status { get; set; }
    public IList<CustomerSub> Subsicrptions { get; set; } = new List<CustomerSub>();
    public IList<Guid> Representative { get; set; }= new List<Guid>();
    public IList<Guid> Visits { get; set; } = new List<Guid>();
    //public IList<FileResponseDto> Files { get; set; } = new List<FileResponseDto>();
   }

public class CustomerSub
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}