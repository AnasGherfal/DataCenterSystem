﻿using Core.Constants;
using Core.Entities;
using Core.Events.Admin;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Management.AdminsManagement.DeleteAdminById;

public sealed record DeleteAdminByIdCommandHandler : IRequestHandler<DeleteAdminByIdCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;
    private readonly UserManager<Account> _userManager;

    public DeleteAdminByIdCommandHandler(UserManager<Account> userManager, AppDbContext dbContext, IClientService client)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(DeleteAdminByIdCommand request, CancellationToken cancellationToken)
    {
        var admin = await _userManager.Users
            .Include(p => p.Admin)
            .SingleOrDefaultAsync(u => u.Id == Guid.Parse(request.Id!)
                && u.AccountType == AccountType.Admin, cancellationToken);
        if (admin == null) throw new NotFoundException("ADMIN_NOT_FOUND");
        var @event = new AdminDeletedEvent(_client.GetIdentifier(), admin.Id, admin.Sequence + 1, new AdminDeletedEventData());
        //TODO: Not Idempotent - Use Transaction Instead
        var result = await _userManager.DeleteAsync(admin);
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()!.Description);
        
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse() { Msg = "SUCCESS" };
    }
}