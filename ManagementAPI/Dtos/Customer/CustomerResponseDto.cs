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
    public IList<SubscriptionRsponseDto> Subsicrptions { get; set; } = new List<SubscriptionRsponseDto>();
    public IList<RepresentativeResponseDto> Representative { get; set; }= new List<RepresentativeResponseDto>();
    public IList<FileResponseDto> Files { get; set; } = new List<FileResponseDto>();
   }
