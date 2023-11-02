using Core.Constants;
using Core.Events.Representative;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.RepresentativeManagement.DeleteRepresentative;

public sealed record DeleteRepresentativeCommandHandler : IRequestHandler<DeleteRepresentativeCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public DeleteRepresentativeCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(DeleteRepresentativeCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Representatives
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("representative not found");
        if (data.Status != GeneralStatus.Active) throw new BadRequestException("Sorry, this representative is not active");
        var customerIsActive = await _dbContext.Customers
            .AnyAsync(p => p.Id == data.CustomerId
                           && p.Status == GeneralStatus.Active, cancellationToken: cancellationToken);
        if (!customerIsActive) throw new BadRequestException("العميل غير موجود");
        var @event = new RepresentativeDeletedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new RepresentativeDeletedEventData());
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