using Infrastructure;
using Infrastructure.Audits.Admin;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.AdminsManagement.LockAdminById;

public sealed record LockAdminByIdCommandHandler : IRequestHandler<LockAdminByIdCommand, MessageResponse>
{
    private readonly DataCenterContext _dbContext;
    private readonly UserManager<Admin> _userManager;

    public LockAdminByIdCommandHandler(UserManager<Admin> userManager, DataCenterContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<MessageResponse> Handle(LockAdminByIdCommand request, CancellationToken cancellationToken)
    {
        var admin = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(request.Id!), cancellationToken);
        if (admin == null) throw new NotFoundException("ADMIN_NOT_FOUND");
        if (!admin.Enabled) throw new BadRequestException("ADMIN_ALREADY_LOCKED");
        admin.Enabled = false;
        var result = await _userManager.UpdateAsync(admin);
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()!.Description);
        await _dbContext.Audits.AddAsync(new AdminLockedAudit("", admin.Id, new AdminLockedAuditData()), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse() { Msg = "SUCCESS" };
    }
}