using ManagementAPI.Dtos.Customer;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface ICustomerService
{
    public Task<MessageResponse> Create(CreateCustomerRequestDto request);
    public Task<MessageResponse> Update(int id , UpdateCustomerRequestDto request);
    public Task<FetchCustomersResponseDto> GetAll(FetchCustomersRequestDto request);
    public Task<MessageResponse> Lock(int id);
    public Task<MessageResponse> Unlock(int id);
    public Task<MessageResponse> Delete(int id);
}
