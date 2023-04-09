using ManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementAPI.Dtos.Subscriptions;
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
        public async Task<ActionResult> CreateSubscription([FromBody]CreateSubscriptionDto request)
        {
            return Ok(await _subscriptionService.CreateSubscription(request));


        }
        [HttpGet]
        public async Task<ActionResult> GetAllSubscription([FromQuery]int pagenum,int pagesize )
        {

            return Ok(await _subscriptionService.GetAllSubscription( pagenum, pagesize ));

        }
        [HttpPut]
        public async Task<OperationResponse> Renew(int id)
        {
            return await _subscriptionService.Renew(id);
        }
    }
}
