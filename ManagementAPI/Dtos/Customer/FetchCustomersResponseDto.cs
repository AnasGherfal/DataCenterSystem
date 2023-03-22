namespace ManagementAPI.Dtos.Customer;

public class FetchCustomersResponseDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public IList<CustomerResponseDto> Content { get; set; }
}
