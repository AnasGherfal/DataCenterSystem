using Core.Dtos;
using Core.Events.Representative;
using Core.Exceptions;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.API.Services.ClientService;

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