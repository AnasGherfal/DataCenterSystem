using ManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using ManagementAPI.Dtos.Subscriptions;
using ManagementAPI.Dtos.File;

namespace ManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly SubscriptionService _subscriptionService;
        public SubscriptionController(SubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        
        [HttpGet]
        //TODO: REVIEW [Bonus]: Add CustomerId Query to allow filtering by CustomerId
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, int pageSize) 
            => Ok(await _subscriptionService.GetAll(pageNumber, pageSize));
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionDto request)
            => Ok(await _subscriptionService.Create(request));

        [HttpPut("{id:int}/Renew")]
        public async Task<IActionResult> Renew(int id) 
            => Ok(await _subscriptionService.Renew(id));

        [HttpPut("{id:int}/lock")]
        public async Task<IActionResult> Lock(int id)
            => Ok(await _subscriptionService.Lock(id));
        
        [HttpPut("{id:int}/unlock")]
        public async Task<IActionResult> Unlock(int id) 
            => Ok(await _subscriptionService.Unlock(id));
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id) 
            => Ok(await _subscriptionService.Remove(id));
        
        [HttpGet("{id:int}/files")]
        public async Task<IActionResult> GetFiles(int id) 
            => Ok(await _subscriptionService.GetFiles(id));
        
        [HttpPost("{id:int}/files")]
        public async Task<IActionResult> UploadFile(int id, [FromForm] FileDto file) 
            => Ok(await _subscriptionService.UploadFile(id, file));
    }
}
