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
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userService.GetById(id));
        }
        [HttpPut("{id:int}/Reset")]
        public async Task<IActionResult> Reset(int id)
        {
            return Ok(await _userService.Reset(id));
        }
        [HttpDelete("{id:int}/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _userService.Delete(id));
        }
        [HttpPut("{id:int}/Edit")]
        public async Task<IActionResult> Edit(int id,[FromBody]UpdateUserRequestDto request)
        {
            return Ok(await _userService.Edit(id, request));
        }
        [HttpPut("{id:int}/Lock")]
        public async Task<IActionResult> Lock(int id)
        {
            return Ok(await _userService.Lock(id));
        }
        [HttpPut("{id:int}/Unlock")]
        public async Task<IActionResult> UnLock(int id)
        {
            return Ok(await _userService.Unlock(id));
        }
    }
}
