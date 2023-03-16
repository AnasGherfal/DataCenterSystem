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
        private readonly CustomerService _service;
        public CustomersController(CustomerService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<OperationResponse> CreateCustomer([FromBody]CreateCustomerDto request)
        {
            return await _service.CreateCustomer(request);
            

        }
    }
}
