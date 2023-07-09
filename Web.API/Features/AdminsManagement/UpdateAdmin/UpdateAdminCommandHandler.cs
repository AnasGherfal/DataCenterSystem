using Common.Constants;
using Infrastructure;
using Infrastructure.Audits.Admin;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.AdminsManagement.UpdateAdmin;

public sealed record UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, MessageResponse>
{
    private readonly DataCenterContext _dbContext;
    private readonly UserManager<Admin> _userManager;

    public UpdateAdminCommandHandler(UserManager<Admin> userManager, DataCenterContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<MessageResponse> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
    {
        var admin = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(request.Id!), cancellationToken);
        if (admin == null) throw new NotFoundException("ADMIN_NOT_FOUND");
        admin.Permissions = (SystemPermissions) request.Permissions!;
        //TODO: Not Idempotent - Use Transaction Instead
        var result = await _userManager.UpdateAsync(admin);
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()!.Description);
        await _dbContext.Audits.AddAsync(new AdminUpdatedAudit("", admin.Id, new AdminUpdatedAuditData()
        {
            Permissions = admin.Permissions,
        }), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse() { Msg = "SUCCESS" };
    }
}