using ManagementAPI.Dtos.Customer;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface ICustomerService
{
    public Task<OperationResponse> Create(CreateCustomerRequestDto request);
    public Task<FetchCustomersResponseDto> GetAll(FetchCustomersRequestDto request);
    public Task<OperationResponse> Lock(int id);
    public Task<OperationResponse> Unlock(int id);
    public Task<OperationResponse> Update(int id , UpdateCustomerRequestDto request);
    public Task<OperationResponse> Delete(int id);
}
