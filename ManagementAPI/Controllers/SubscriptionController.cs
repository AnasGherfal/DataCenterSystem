using ManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementAPI.Dtos.Subscriptions;
using ManagementAPI.Dtos.File;
using Shared.Dtos;

namespace ManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        SubscriptionService _subscriptionService;
        public SubscriptionController(SubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;

        }
        [HttpPost]
        public async Task<ActionResult> CreateSubscription([FromBody] CreateSubscriptionDto request)
        {
            return Ok(await _subscriptionService.CreateSubscription(request));


        }
        [HttpGet]
        public async Task<ActionResult> GetAllSubscription([FromQuery] int pagenum, int pagesize)
        {

            return Ok(await _subscriptionService.GetAllSubscription(pagenum, pagesize));

        }
        [HttpPut("Renew")]
        public async Task<OperationResponse> Renew(int id)
        {
            return await _subscriptionService.Renew(id);
        }
        [HttpPost("file")]
        public async Task<OperationResponse> UploaddFile([FromForm]FileDto file,int id)
        {
            return await _subscriptionService.UploadFile(id,file);
        }
        [HttpGet("Subscription File")]
        public async Task<ActionResult> GetAllFile([FromQuery]FetchSubscriptionRequestDto request)
        {
            return Ok(await _subscriptionService.GetFile(request));
        }
        [HttpGet("{id}")]
        public async Task<SubscriptionFileResponsDto> GetFileBySubscriptionId([FromQuery]int id)
        {
            return await _subscriptionService.GetFileBySubscriptionId(id);
        }
        [HttpPut("{id}/lock")]
        public async Task<OperationResponse> Lock(int id)
        {
            return await _subscriptionService.Lock(id);
        }
        [HttpPut("{id}/unlock")]
        public async Task<OperationResponse> Unlock(int id)
        {
            return await _subscriptionService.Unlock(id);
        }
        [HttpDelete]
        public async Task<OperationResponse> Remove(int id)
        {
            return await _subscriptionService.Remove(id);
        }
    }
}
