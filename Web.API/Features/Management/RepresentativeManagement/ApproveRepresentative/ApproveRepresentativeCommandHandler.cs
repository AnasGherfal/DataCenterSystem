using Core.Constants;
using Core.Events.Representative;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Management.RepresentativeManagement.ApproveRepresentative;

public sealed record ApproveRepresentativeCommandHandler : IRequestHandler<ApproveRepresentativeCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public ApproveRepresentativeCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(ApproveRepresentativeCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Representatives.SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("Representative not found");
        if (data.Status != GeneralStatus.Requested) throw new BadRequestException("Sorry, this representative isn't in requesting state");
        var @event = new RepresentativeApprovedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new RepresentativeApprovedEventData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "Representative Approved successfully!",
        };
    }
}