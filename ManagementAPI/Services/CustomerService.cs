using AutoMapper;
using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System.Net;

namespace ManagementAPI.Services;

public class CustomerService
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;
    public CustomerService(DataCenterContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<OperationResponse> CreateCustomer(CreateCustomerDto request)
    {

        var NewCustomer = _mapper.Map<Customer>(request);
        if (NewCustomer == null)
            return new OperationResponse()
            { Msg = "طلبك غير صالح يرجى إدخال بيانات العميل", StatusCode = HttpStatusCode.BadRequest };

        await _dbContext.Customers.AddAsync(NewCustomer);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse() { Msg = "ok", StatusCode = HttpStatusCode.OK };
    }



    public async Task<FetchCustomersResponseDto> GetAllCustomer(int pgSize, int pgNum)
    {

        var CustQuery = await (from Cust in _dbContext.Customers
                               .OrderBy(x => x.Id)
                               select Cust)
                               .Skip(pgSize * (pgNum - 1))
                               .Take(pgSize)
                               .ToListAsync();

        var result = _mapper.Map<List<CustomerResponseDto>>(CustQuery);
        var totalCount = (from Cst in CustQuery select Cst).Count();
        var totalpages = (int)Math.Ceiling(totalCount / 25.00);

        return new FetchCustomersResponseDto() { Content = result, CurrentPage = pgNum, TotalPages = totalpages };
    }

    /*  public async Task<CustomerResponseDto> GetCustomer(int id)
      {
          throw new NotImplementedException();
      }


      public async Task<OperationResponse> UpdateCustomer(int id, UpdateCustomerRequestDto request)
      {
          throw new NotImplementedException();
      }

  }
      public async Task<OperationResponse> DeleteCustomer(int id)
      {
          throw new NotImplementedException();
      }*/

}