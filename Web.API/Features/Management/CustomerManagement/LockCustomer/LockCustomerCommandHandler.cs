using Core.Constants;
using Core.Entities;
using Core.Events.Customer;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.CustomerManagement.LockCustomer;

public sealed record LockCustomerCommandHandler : IRequestHandler<LockCustomerCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public LockCustomerCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(LockCustomerCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Users
            .Include(p => p.Customer)
            .SingleOrDefaultAsync(p => p.Id == id
                && p.AccountType == AccountType.Customer, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("Customer not found");
        if (data.Customer.Status == GeneralStatus.Locked) throw new BadRequestException("Sorry, this Customer is already locked");
        if (data.Customer.Status != GeneralStatus.Active) throw new BadRequestException("Sorry, this cannot be locked");
        var @event = new CustomerLockedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new CustomerLockedEventData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "Customer locked successfully!",
        };
    }
}