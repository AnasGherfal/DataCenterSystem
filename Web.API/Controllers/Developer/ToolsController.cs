using Core.Constants;
using Core.Entities;
using Core.Events.Admin;
using Core.Exceptions;
using Core.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.API.Abstracts;
using Web.API.Features.VisitsManagement.CreateVisit;

namespace Web.API.Controllers.Developer;


[ApiController]
public class ToolsController : ManagementController
{
    private readonly UserManager<Account> _userManager;
    
    public ToolsController(UserManager<Account> userManager)
    {
        _userManager = userManager;
    }

    [HttpDelete("Reset-Admin-Password-By-Id")]
    public async Task<MessageResponse> Create([FromQuery] string userId)
    {
        var admin = await _userManager.Users
            .SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId!)
                                       && u.AccountType == AccountType.Admin);
        if (admin == null) throw new NotFoundException("ADMIN_NOT_FOUND");
        var newPassword = GeneratePassword();
        var token = await _userManager.GeneratePasswordResetTokenAsync(admin);
        var result = await _userManager.ResetPasswordAsync(admin, token, newPassword);
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()!.Description);
        return new MessageResponse()
        {
            Msg = newPassword
        };
    }
    
    private static readonly Random Random = new();
    private string GeneratePassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var newPassword = (Enumerable.Repeat(chars, 8).Select(s => s[Random.Next(s.Length)]).ToArray());
        return  new string(newPassword);
    }
}