using ManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using ManagementAPI.Dtos.Subscriptions;
using ManagementAPI.Dtos;
using Infrastructure.Models;

namespace ManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _service;
        public SubscriptionController(SubscriptionService service)
        {
            _service = service;
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]  CreateSubscriptionRequestDto request)
            => Ok(await _service.Create(request));
        [HttpGet]
        //TODO: REVIEW [Bonus]: Add CustomerId Query to allow filtering by CustomerId
        public async Task<IActionResult> GetAll([FromQuery] FetchSubscriptionRequestDto request) 
            => Ok(await _service.GetAll(request));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
     => Ok(await _service.GetById(id));

        [HttpPut("{id:int}/Renew")]
        public async Task<IActionResult> Renew(Guid id) 
            => Ok(await _service.Renew(id));

        [HttpPut("{id:int}/lock")]
        public async Task<IActionResult> Lock(Guid id)
            => Ok(await _service.Lock(id));
        
        [HttpPut("{id:int}/unlock")]
        public async Task<IActionResult> Unlock(Guid id) 
            => Ok(await _service.Unlock(id));
        
        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Delete(Guid id) 
            => Ok(await _service.Delete(id));
        [HttpDelete("{id:int}/deleteFile")]
        public async Task<IActionResult> DeleteFile(Guid id)
            => Ok(await _service.DeleteFile(id));

        [HttpGet("files")]
        public async Task<IActionResult> GetFiles([FromQuery]FetchSubscriptionRequestDto request) 
            => Ok(await _service.GetFiles(request));
        
       /* [HttpPost("{id:int}/files")]
        public async Task<IActionResult> UploadFile(int id, [FromForm] FileDto file) 
            => Ok(await _subscriptionService.UploadFile(id, file));*/
        [HttpGet("{id:int}/fileById")]
        public async Task<IActionResult> GetFileById(Guid id)
            => Ok(await _service.GetFileById(id));

        [HttpPut("{id:int}/update")]
        public async Task<IActionResult> Update(Guid id,UpdateSubscriptionRequestDto request)
           => Ok(await _service.Update(id,request));
        [HttpPut("{id:int}/updateFile")]
        public async Task<IActionResult> UpdateFile(Guid id, [FromForm] FileRequestDto request)
           => Ok(await _service.UpdateFile(id, request));
        [HttpGet("Download")]
        public async Task<IActionResult> Download(Guid id)
            => Ok(await _service.Download(id));
    }
}
