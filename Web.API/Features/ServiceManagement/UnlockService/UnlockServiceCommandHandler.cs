﻿using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Events.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.ClientService;

namespace Web.API.Features.ServiceManagement.UnlockService;

public sealed record UnlockServiceCommandHandler : IRequestHandler<UnlockServiceCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public UnlockServiceCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(UnlockServiceCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("!عذرًا لا وجود لباقة بهذا الرقم");
        if (data.Status != GeneralStatus.Locked) throw new BadRequestException($"! {data.Name}: عذرًا هذه الباقة غير مقيدة");
        var @event = new ServiceUnlockedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new ServiceUnlockedEventData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تمت إضافة الباقة بنجاح",
        };
    }
}