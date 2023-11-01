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

namespace Web.API.Features.CustomerManagement.DeleteCustomer;

public sealed record DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public DeleteCustomerCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Users
            .Include(p => p.Customer)
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("Customer not found");
        var hasSubscriptions = await _dbContext.Subscriptions.AnyAsync(p => p.CustomerId == id, cancellationToken: cancellationToken);
        if (hasSubscriptions) throw new BadRequestException("لا يمكن حدف العميل لوجود اشتراكات مرتبطة به");
        var hasRepresentatives = await _dbContext.Representatives.AnyAsync(p => p.CustomerId == id, cancellationToken: cancellationToken);
        if (hasRepresentatives) throw new BadRequestException("لا يمكن حدف العميل لوجود مندوبين مرتبطين به");
        var hasVisits = await _dbContext.Visits.AnyAsync(p => p.CustomerId == id, cancellationToken: cancellationToken);
        if (hasVisits) throw new BadRequestException("لا يمكن حدف العميل لوجود زيارات مرتبطة به");
        var @event = new CustomerDeletedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new CustomerDeletedEventData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تمت حدف بنجاح",
        };
    }
}