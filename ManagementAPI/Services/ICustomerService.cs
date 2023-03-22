using ManagementAPI.Dtos.Customer;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface ICustomerService
{
    public Task<OperationResponse> CreateCustomer(CreateCustomerRequestDto request);
    public Task<FetchCustomersResponseDto> GetAllCustomer(int pgSize, int pgNum);
    public Task<CustomerResponseDto> GetCustomer(int id);
    public Task<OperationResponse> UpdateCustomer(int id , EditCustomerRequestDto request);
    public Task<OperationResponse> DeleteCustomer(int id);

    
}
