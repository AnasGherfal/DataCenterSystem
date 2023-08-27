using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Events.Admin;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.ClientService;

namespace Web.API.Features.AdminsManagement.UnlockAdminById;

public sealed record UnlockAdminByIdCommandHandler : IRequestHandler<UnlockAdminByIdCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;
    private readonly UserManager<Admin> _userManager;

    public UnlockAdminByIdCommandHandler(UserManager<Admin> userManager, AppDbContext dbContext, IClientService client)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(UnlockAdminByIdCommand request, CancellationToken cancellationToken)
    {
        var admin = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(request.Id!), cancellationToken);
        if (admin == null) throw new NotFoundException("ADMIN_NOT_FOUND");
        if (admin.Enabled) throw new BadRequestException("ADMIN_ALREADY_UNLOCKED");
        var @event = new AdminUnlockedEvent(_client.GetIdentifier(), admin.Id, admin.Sequence + 1, new AdminUnlockedEventData());
        admin.Apply(@event);
        //TODO: Not Idempotent - Use Transaction Instead
        var result = await _userManager.UpdateAsync(admin);
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()!.Description);
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse() { Msg = "SUCCESS" };
    }
}