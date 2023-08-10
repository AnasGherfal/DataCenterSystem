using Infrastructure.Constants;
using ManagementAPI.Dtos.Representative;
using ManagementAPI.Dtos.Subscriptions;
using ManagementAPI.Dtos.Visit;
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
    public IList<CustomerSubscriptionRsponseDto> Subsicrptions { get; set; } = new List<CustomerSubscriptionRsponseDto>();
    public IList<RepresentativeResponseDto> Representative { get; set; }= new List<RepresentativeResponseDto>();
    public IList<FileResponseDto> Files { get; set; } = new List<FileResponseDto>();
   }
public class CustomerSubscriptionRsponseDto
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; }=string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public GeneralStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DaysRemainig { get; set; }
}