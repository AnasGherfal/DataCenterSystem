using ManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using ManagementAPI.Dtos.Subscriptions;
using Shared.Dtos;
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
        public async Task<IActionResult> GetById(string id) => Ok(await _service.GetById(Guid.Parse(id)));

        [HttpPut("{id}/Renew")]
        public async Task<IActionResult> Renew(string id) => Ok(await _service.Renew(Guid.Parse(id)));

        [HttpPut("{id}/lock")]
        public async Task<IActionResult> Lock(string id) => Ok(await _service.Lock(Guid.Parse(id)));
        
        [HttpPut("{id}/unlock")]
        public async Task<IActionResult> Unlock(string id) => Ok(await _service.Unlock(Guid.Parse(id)));
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) => Ok(await _service.Delete(Guid.Parse(id)));
        [HttpDelete("{id:int}/deleteFile")]
        public async Task<IActionResult> DeleteFile(string id) => Ok(await _service.DeleteFile(Guid.Parse(id)));

        [HttpGet("files")]
        public async Task<IActionResult> GetFiles([FromQuery]FetchSubscriptionRequestDto request) 
            => Ok(await _service.GetFiles(request));
        
       /* [HttpPost("{id:int}/files")]
        public async Task<IActionResult> UploadFile(int id, [FromForm] FileDto file) 
            => Ok(await _subscriptionService.UploadFile(id, file));*/
        [HttpGet("{id:int}/fileById")]
        public async Task<IActionResult> GetFileById(string id) => Ok(await _service.GetFileById(Guid.Parse(id)));

        [HttpPut("{id:int}/update")]
        public async Task<IActionResult> Update(string id,UpdateSubscriptionRequestDto request)
           => Ok(await _service.Update(Guid.Parse(id),request));
        [HttpPut("{id:int}/updateFile")]
        public async Task<IActionResult> UpdateFile(string id, [FromForm] FileRequestDto request)
           => Ok(await _service.UpdateFile(Guid.Parse(id), request));
        [HttpGet("Download")]
        public async Task<IActionResult> Download(string id) => Ok(await _service.Download(Guid.Parse(id)));
    }
}
