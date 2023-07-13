using FluentValidation;
using ManagementAPI.Dtos.Subscriptions;

namespace ManagementAPI.Dtos.Service;

public class FetchServicesResponseDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public IList<ServiceResponseDto> Content { get; set; } = new List<ServiceResponseDto>();
}


