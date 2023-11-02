using Core.Constants;
using Core.Events.Representative;
using Core.Exceptions;
using Core.Interfaces.Dtos;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.RepresentativeManagement.UpdateRepresentativeFile;

public sealed record UpdateRepresentativeFileCommandHandler : IRequestHandler<UpdateRepresentativeFileCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;
    private readonly IUploadFileService _uploadFile;
    
    public UpdateRepresentativeFileCommandHandler(AppDbContext dbContext, IUploadFileService uploadFile, IClientService client)
    {
        _dbContext = dbContext;
        _uploadFile = uploadFile;
        _client = client;
    }

    public async Task<MessageResponse> Handle(UpdateRepresentativeFileCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var fileId = Guid.Parse(request.Id!);
        var data = await _dbContext.Representatives
            .Include(p => p.Documents)
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("Representative not found");
        if (data.Status != GeneralStatus.Active) throw new BadRequestException("Sorry, this representative is not active");
        var customerIsActive = await _dbContext.Customers
            .AnyAsync(p => p.Id == data.CustomerId
                           && p.Status == GeneralStatus.Active, cancellationToken: cancellationToken);
        if (!customerIsActive) throw new BadRequestException("العميل غير موجود");
        var uploadPath = await _uploadFile.UploadFiles(StorageType.RepresentativeFile, new List<FileStorageUploadRequest>()
        {
            new(Guid.NewGuid(), request.File!, (short) request.DocType!.Value)
        });
        if (uploadPath == null) throw new BadRequestException("حدث خطأ أثناء رفع الملف");
        var @event = new RepresentativeFileUpdatedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new RepresentativeFileUpdatedEventData()
        {
            OldFileIdentifier = fileId,
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