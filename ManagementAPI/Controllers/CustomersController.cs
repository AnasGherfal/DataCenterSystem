using Infrastructure;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace ManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        public CustomersController(ICustomerService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<OperationResponse> CreateCustomer([FromBody]CreateCustomerRequestDto request)
        {
            return await _service.CreateCustomer(request);
            

        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] FetchCustomersRequestDto filter) 
        {
            var result=await _service.GetAllCustomer(filter.PageSize,filter.PageNumber);
            return Ok(result);
             
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomer(int id, [FromBody] EditCustomerRequestDto request)
        {
          
            return Ok(await _service.UpdateCustomer(id, request));
            
        }
    }
}
