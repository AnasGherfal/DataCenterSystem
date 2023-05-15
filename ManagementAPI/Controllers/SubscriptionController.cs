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
        public async Task<IActionResult> GetAll([FromQuery] FetchSubscriptionRequestDto request) 
            => Ok(await _subscriptionService.GetAll(request));
        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]  FileDto fileRequest,[FromBody] CreateSubscriptionDto request)
            => Ok(await _subscriptionService.Create(fileRequest, request));

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
        
        [HttpGet("{Id:int}/files")]
        public async Task<IActionResult> GetFiles([FromQuery]FetchSubscriptionRequestDto request) 
            => Ok(await _subscriptionService.GetFiles(request));
        
       /* [HttpPost("{id:int}/files")]
        public async Task<IActionResult> UploadFile(int id, [FromForm] FileDto file) 
            => Ok(await _subscriptionService.UploadFile(id, file));*/
        [HttpGet("{id:int}/fileById")]
        public async Task<IActionResult> GetFileById(int id)
            => Ok(await _subscriptionService.GetFileById(id));
        [HttpGet("page")]
        public async Task<IActionResult> GetPageContent(string url)
           => Ok(await _subscriptionService.GetPageContent(url));
    }
}
