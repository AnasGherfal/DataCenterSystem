using Infrastructure;
using Infrastructure.Audits.Service;
using Infrastructure.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.ServiceManagement.LockService;

public sealed record LockServiceCommandHandler : IRequestHandler<LockServiceCommand, MessageResponse>
{
    private readonly DataCenterContext _dbContext;

    public LockServiceCommandHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MessageResponse> Handle(LockServiceCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("!عذرًا لا وجود لباقة بهذا الرقم");
        if (data.Status == GeneralStatus.Locked) throw new BadRequestException($"! {data.Name}: عذرًا هذه الباقة مقيدة");
        var @event = new ServiceLockedAudit("", Guid.NewGuid(), new ServiceLockedAuditData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Audits.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تمت إضافة الباقة بنجاح",
        };
    }
}