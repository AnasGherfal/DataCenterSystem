using ManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using ManagementAPI.Dtos.Subscriptions;
using ManagementAPI.Dtos;

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
        public async Task<IActionResult> Create([FromForm]  CreateSubscriptionRequestDto request)
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
        
        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Remove(int id) 
            => Ok(await _subscriptionService.Remove(id));
        [HttpDelete("{id:int}/deleteFile")]
        public async Task<IActionResult> RemoveFile(int id)
            => Ok(await _subscriptionService.RemoveFile(id));

        [HttpGet("files")]
        public async Task<IActionResult> GetFiles([FromQuery]FetchSubscriptionRequestDto request) 
            => Ok(await _subscriptionService.GetFiles(request));
        
       /* [HttpPost("{id:int}/files")]
        public async Task<IActionResult> UploadFile(int id, [FromForm] FileDto file) 
            => Ok(await _subscriptionService.UploadFile(id, file));*/
        [HttpGet("{id:int}/fileById")]
        public async Task<IActionResult> GetFileById(int id)
            => Ok(await _subscriptionService.GetFileById(id));

        [HttpPut("{id:int}/update")]
        public async Task<IActionResult> Update(int id,UpdateSubscriptionRequestDto request)
           => Ok(await _subscriptionService.Update(id,request));
        [HttpPut("{id:int}/updateFile")]
        public async Task<IActionResult> UpdateFile(int id, [FromForm] FileRequestDto request)
           => Ok(await _subscriptionService.UpdateFile(id, request));
        [HttpGet("Download")]
        public async Task<IActionResult> Download(int id)
            => Ok(await _subscriptionService.Download(id));
    }
}
