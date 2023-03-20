using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using Shared.Dtos;
using System.Net;

namespace ManagementAPI.Services
{
    public class CustomerService
    {
        private readonly DataCenterContext _dbContext;
        public CustomerService(DataCenterContext dbContext)
        {
            _dbContext = dbContext;
        }
        public  async Task<OperationResponse> CreateCustomer(CreateCustomerDto request)
        {
            //todo: check if exist
            
            await _dbContext.Customers.AddAsync(new Customer() {
                Name = request.Name,
                Email = request.Email,
                PrimaryPhone = request.PrimaryPhone,
                SecondaryPhone = request.SecondaryPhone,

            });
            await _dbContext.SaveChangesAsync();
            return new OperationResponse() { Msg = "ok" ,StatusCode=HttpStatusCode.OK};
        }
    }
}
