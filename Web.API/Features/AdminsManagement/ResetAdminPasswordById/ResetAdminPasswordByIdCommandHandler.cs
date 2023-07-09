using Infrastructure;
using Infrastructure.Audits.Admin;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.AdminsManagement.ResetAdminPasswordById;

public sealed record ResetAdminPasswordByIdCommandHandler : IRequestHandler<ResetAdminPasswordByIdCommand, ContentResponse<string>>
{
    private readonly DataCenterContext _dbContext;
    private readonly UserManager<Admin> _userManager;

    public ResetAdminPasswordByIdCommandHandler(UserManager<Admin> userManager, DataCenterContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<string>> Handle(ResetAdminPasswordByIdCommand request, CancellationToken cancellationToken)
    {
        var admin = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(request.Id!), cancellationToken);
        if (admin == null) throw new NotFoundException("ADMIN_NOT_FOUND");
        var newPassword = GeneratePassword();
        var token = await _userManager.GeneratePasswordResetTokenAsync(admin);
        //TODO: Not Idempotent - Use Transaction Instead
        var result = await _userManager.ResetPasswordAsync(admin, token, newPassword);
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()!.Description);
        await _dbContext.Audits.AddAsync(new AdminPasswordResetAudit("", admin.Id, new AdminPasswordResetAuditData()
        {
            NewPassword = newPassword,
        }), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ContentResponse<string>("SUCCESS", "");
    }
    
    private static readonly Random Random = new();
    private string GeneratePassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var newPassword = (Enumerable.Repeat(chars, 8).Select(s => s[Random.Next(s.Length)]).ToArray());
        return  new string(newPassword);
    }
}