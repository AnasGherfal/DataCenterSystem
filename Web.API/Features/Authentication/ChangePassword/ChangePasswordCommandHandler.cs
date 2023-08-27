﻿using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Events.Admin;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.ClientService;

namespace Web.API.Features.Authentication.ChangePassword;

public sealed record ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;
    private readonly UserManager<Admin> _userManager;

    public ChangePasswordCommandHandler(UserManager<Admin> userManager, AppDbContext dbContext, IClientService client)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var admin = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == _client.GetIdentifier(), cancellationToken);
        if (admin == null) throw new NotFoundException("ADMIN_NOT_FOUND");
        var @event = new AdminPasswordChangedEvent(_client.GetIdentifier(), admin.Id, admin.Sequence + 1, new AdminPasswordChangedEventData()
        {
            NewPassword = request.NewPassword!,
        });
        var result = await _userManager.ChangePasswordAsync(admin, request.OldPassword!, request.NewPassword!);
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()!.Description);
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "PASSWORD_CHANGED",
        };
    }
}