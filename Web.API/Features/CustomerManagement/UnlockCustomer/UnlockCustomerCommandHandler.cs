﻿using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Events.Customer;
using Infrastructure.Events.Subscription;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.ClientService;

namespace Web.API.Features.CustomerManagement.UnlockCustomer;

public sealed record UnlockCustomerCommandHandler : IRequestHandler<UnlockCustomerCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public UnlockCustomerCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(UnlockCustomerCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Customers.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("Customer not found");
        if (data.Status != GeneralStatus.Locked) throw new BadRequestException("Customer is not locked");
        var @event = new CustomerUnlockedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new CustomerUnlockedEventData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "subscription unlocked successfully!"
        };
    }
}