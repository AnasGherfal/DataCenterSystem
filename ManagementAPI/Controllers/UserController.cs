using ManagementAPI.Dtos.User;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace ManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService  _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequestDto request)
        {
            return Ok(await _userService.Create(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FetchUsersRequestDto request)
        {
            return Ok(await _userService.GetAll(request));
        }
        [HttpGet("{id:int}/Get")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _userService.GetById(Guid.Parse(id)));
        }
        [HttpPut("{id:int}/Reset")]
        public async Task<IActionResult> Reset(string id)
        {
            return Ok(await _userService.Reset(Guid.Parse(id)));
        }
        [HttpDelete("{id:int}/Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _userService.Delete(Guid.Parse(id)));
        }
        [HttpPut("{id:int}/Edit")]
        public async Task<IActionResult> Edit(string id,[FromBody]UpdateUserRequestDto request)
        {
            return Ok(await _userService.Edit(Guid.Parse(id), request));
        }
        [HttpPut("{id:int}/Lock")]
        public async Task<IActionResult> Lock(string id)
        {
            return Ok(await _userService.Lock(Guid.Parse(id)));
        }
        [HttpPut("{id:int}/Unlock")]
        public async Task<IActionResult> UnLock(string id)
        {
            return Ok(await _userService.Unlock(Guid.Parse(id)));
        }
    }
}
