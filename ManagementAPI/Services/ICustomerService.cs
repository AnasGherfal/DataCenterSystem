using ManagementAPI.Dtos.Customer;
using Shared.Constants;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface ICustomerService
{
    public Task<MessageResponse> Create(CreateCustomerRequestDto request);
    public Task<CustomerResponseDto> GetById(Guid id);
    public Task<FetchCustomersResponseDto> GetAll(FetchCustomersRequestDto request);
    public Task<MessageResponse> Update(Guid id , UpdateCustomerRequestDto request);
    public Task<MessageResponse> Lock(Guid id);
    public Task<MessageResponse> Unlock(Guid id);
    public Task<MessageResponse> Delete(Guid id);
}
