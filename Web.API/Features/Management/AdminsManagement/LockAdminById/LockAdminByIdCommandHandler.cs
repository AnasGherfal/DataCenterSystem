using Core.Constants;
using Core.Entities;
using Core.Events.Admin;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Management.AdminsManagement.LockAdminById;

public sealed record LockAdminByIdCommandHandler : IRequestHandler<LockAdminByIdCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;
    private readonly UserManager<Account> _userManager;

    public LockAdminByIdCommandHandler(UserManager<Account> userManager, AppDbContext dbContext, IClientService client)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(LockAdminByIdCommand request, CancellationToken cancellationToken)
    {
        var admin = await _userManager.Users
            .Include(p => p.Admin)
            .SingleOrDefaultAsync(u => u.Id == Guid.Parse(request.Id!)
                && u.AccountType == AccountType.Admin, cancellationToken);
        if (admin == null) throw new NotFoundException("ADMIN_NOT_FOUND");
        if (!admin.Enabled) throw new BadRequestException("ADMIN_ALREADY_LOCKED");
        var @event = new AdminLockedEvent(_client.GetIdentifier(), admin.Id, admin.Sequence + 1, new AdminLockedEventData());
        admin.Apply(@event);
        var result = await _userManager.UpdateAsync(admin);
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()!.Description);
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse() { Msg = "SUCCESS" };
    }
}