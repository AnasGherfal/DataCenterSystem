using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Events.Subscription;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.ClientService;
using Web.API.Services.UploadService;
using Web.API.Services.UploadService.Dtos;

namespace Web.API.Features.SubscriptionManagement.UpdateSubscriptionFile;

public sealed record UpdateSubscriptionFileCommandHandler : IRequestHandler<UpdateSubscriptionFileCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;
    private readonly IUploadFileService _uploadFile;
    
    public UpdateSubscriptionFileCommandHandler(AppDbContext dbContext, IUploadFileService uploadFile, IClientService client)
    {
        _dbContext = dbContext;
        _uploadFile = uploadFile;
        _client = client;
    }

    public async Task<MessageResponse> Handle(UpdateSubscriptionFileCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var fileId = Guid.Parse(request.Id!);
        var data = await _dbContext.Subscriptions
            .Include(p => p.Documents)
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("Subscription not found");
        if (data.Status != GeneralStatus.Active) throw new BadRequestException("Sorry, this subscription is not active");
        var uploadPath = await _uploadFile.UploadFiles(StorageType.SubscriptionFile, new List<FileStorageUploadRequest>()
        {
            new(Guid.NewGuid(), request.File!, (short) request.DocType!.Value)
        });
        if (uploadPath == null) throw new BadRequestException("حدث خطأ أثناء رفع الملف");
        var @event = new SubscriptionFileUpdatedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new SubscriptionFileUpdatedEventData()
        {
            FileIdentifier = uploadPath.First().Id,
            FileLink = uploadPath.First().Link,
            FileType = request.DocType!.Value,
        });
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "Subscription file updated successfully!",
        };
    }
}