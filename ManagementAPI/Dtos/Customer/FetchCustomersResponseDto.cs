namespace ManagementAPI.Dtos.Customer;

public record FetchCustomersResponseDto(int CurrentPage, int TotalPages, IList<CustomerResponseDto> Content);

