using Infrastructure;
using Infrastructure.Audits.Service;
using Infrastructure.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.ServiceManagement.UnlockService;

public sealed record UnlockServiceCommandHandler : IRequestHandler<UnlockServiceCommand, MessageResponse>
{
    private readonly DataCenterContext _dbContext;

    public UnlockServiceCommandHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MessageResponse> Handle(UnlockServiceCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("!عذرًا لا وجود لباقة بهذا الرقم");
        if (data.Status != GeneralStatus.Locked) throw new BadRequestException($"! {data.Name}: عذرًا هذه الباقة غير مقيدة");
        var @event = new ServiceUnlockedAudit("", Guid.NewGuid(), new ServiceUnlockedAuditData());
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