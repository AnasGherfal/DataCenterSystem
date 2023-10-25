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

namespace Web.API.Features.CustomerManagement.UnlockCustomer;

public sealed record UnlockCustomerCommandHandler : IRequestHandler<UnlockCustomerCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;
    private readonly UserManager<Customer> _customerManager;

    public UnlockCustomerCommandHandler(AppDbContext dbContext, IClientService client, UserManager<Customer> customerManager)
    {
        _dbContext = dbContext;
        _client = client;
        _customerManager = customerManager;
    }

    public async Task<MessageResponse> Handle(UnlockCustomerCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _customerManager.Users.SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
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