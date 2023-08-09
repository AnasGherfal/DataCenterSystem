using Infrastructure;
using Infrastructure.Audits.Service;
using Infrastructure.Audits.Subscription;
using Infrastructure.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.UploadService;

namespace Web.API.Features.SubscriptionManagement.UpdateSubscriptionFile;

public sealed record UpdateSubscriptionFileCommandHandler : IRequestHandler<UpdateSubscriptionFileCommand, MessageResponse>
{
    private readonly DataCenterContext _dbContext;
    private readonly IUploadFileService _uploadFile;
    
    public UpdateSubscriptionFileCommandHandler(DataCenterContext dbContext, IUploadFileService uploadFile)
    {
        _dbContext = dbContext;
        _uploadFile = uploadFile;
    }

    public async Task<MessageResponse> Handle(UpdateSubscriptionFileCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var fileId = Guid.Parse(request.Id!);
        var data = await _dbContext.Subscriptions
            .Include(p => p.SubscriptionFile)
            .SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("Subscription not found");
        if (data.Status != GeneralStatus.Active) throw new BadRequestException("Sorry, this subscription is not active");
        if (data.SubscriptionFile.Id != fileId) throw new BadRequestException("Sorry, this subscription file is not valid");
        var uploadPath = await _uploadFile.Upload(request.File!, StorageType.SubscriptionFile);
        var @event = new SubscriptionFileUpdatedAudit("", Guid.NewGuid(), new SubscriptionFileUpdatedAuditData()
        {
            FileIdentifier = fileId,
            FileType = request.DocType!.Value,
            File = uploadPath,
        });
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Audits.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "Subscription file updated successfully!",
        };
    }
}