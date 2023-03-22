using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
<<<<<<< Updated upstream
=======
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
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
=======
            var NewCustomer=_mapper.Map<Customer>(request);
            if(NewCustomer == null)
                return new OperationResponse()
                { Msg="طلبك غير صالح يرجى إدخال بيانات العميل",StatusCode=HttpStatusCode.BadRequest};

            await _dbContext.Customers.AddAsync(NewCustomer);
            await _dbContext.SaveChangesAsync();
            return new OperationResponse() { Msg = "ok" ,StatusCode=HttpStatusCode.OK};
        }

        
       
        public async Task<FetchCustomersResponseDto> GetAllCustomer(int pgSize , int pgNum)
        {
            
            var CustQuery =await (from Cust in _dbContext.Customers
                                   .OrderBy(x => x.Id)
                                    select Cust)
                                   .Skip(pgSize * (pgNum - 1))
                                   .Take(pgSize)
                                   .ToListAsync();

            var result = _mapper.Map<List<CustomerResponseDto>>(CustQuery);
            var totalCount = (from Cst in CustQuery select Cst).Count();
            var totalpages=(int)Math.Ceiling(totalCount / 25.00);

            return new FetchCustomersResponseDto() { Content = result, CurrentPage = pgNum, TotalPages = totalpages };
        }
        
        public async Task<CustomerResponseDto> GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        
        public async Task<OperationResponse> UpdateCustomer(int id, UpdateCustomerRequestDto request)
        {
            throw new NotImplementedException();
        }
>>>>>>> Stashed changes
    }
        public async Task<OperationResponse> DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

}
