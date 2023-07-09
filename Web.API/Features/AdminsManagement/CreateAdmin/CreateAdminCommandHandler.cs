using Common.Constants;
using Infrastructure;
using Infrastructure.Audits.Admin;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.AdminsManagement.CreateAdmin;

public sealed record CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, ContentResponse<CreateAdminCommandResponse>>
{
    private readonly DataCenterContext _dbContext;
    private readonly UserManager<Admin> _userManager;

    public CreateAdminCommandHandler(UserManager<Admin> userManager, DataCenterContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<CreateAdminCommandResponse>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var admin = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        if (admin != null) throw new BadRequestException("EMAIL_ALREADY_EXIST");
        var newAdmin = new Admin()
        {
            Email = request.Email!,
            DisplayName = request.FullName!,
            EmployeeId = request.EmpId!.Value,
            Permissions = (SystemPermissions) request.Permissions!.Value,
            UserName = Guid.NewGuid().ToString().Replace("-", ""),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            CreatedOn = DateTime.UtcNow,
        };
        var password = GeneratePassword();
        //TODO: Not Idempotent - Use Transaction Instead
        var result = await _userManager.CreateAsync(newAdmin, password);
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()!.Description);
        await _dbContext.Audits.AddAsync(new AdminCreatedAudit("", newAdmin.Id, new AdminCreatedAuditData()
        {
            FullName = newAdmin.DisplayName,
            Email = newAdmin.Email,
            Permissions = newAdmin.Permissions,
            EmpId = newAdmin.EmployeeId,
            Password = password,
        }), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        var responseContent = new CreateAdminCommandResponse()
        {
            UserId = newAdmin.Id.ToString(),
            UserPassword = password,
        };
        return new ContentResponse<CreateAdminCommandResponse>("SUCCESS", responseContent);
    }

    private static readonly Random Random = new();
    private string GeneratePassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var newPassword = (Enumerable.Repeat(chars, 8).Select(s => s[Random.Next(s.Length)]).ToArray());
        return  new string(newPassword);
    }
}