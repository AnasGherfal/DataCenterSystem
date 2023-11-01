using Core.Constants;
using Core.Entities;
using Core.Events.Customer;
using Core.Exceptions;
using Core.Interfaces.Dtos;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.CustomerManagement.UpdateCustomerFile;

public sealed record UpdateCustomerFileCommandHandler : IRequestHandler<UpdateCustomerFileCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;
    private readonly IUploadFileService _uploadFile;
    
    public UpdateCustomerFileCommandHandler(AppDbContext dbContext, IUploadFileService uploadFile, IClientService client)
    {
        _dbContext = dbContext;
        _uploadFile = uploadFile;
        _client = client;
    }

    public async Task<MessageResponse> Handle(UpdateCustomerFileCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.FileId!);
        var data = await _dbContext.Users
            .Include(p => p.Customer)
            .ThenInclude(p => p.Documents)
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        var docType = data.Customer.Documents.FirstOrDefault(p => p.Id.Equals(request.FileId)).FileType!;
        if (data == null) throw new NotFoundException("Customer not found");
        if (data.Customer.Status != GeneralStatus.Active) throw new BadRequestException("Sorry, this Customer is not active");
        var uploadPath = await _uploadFile.UploadFiles(StorageType.RepresentativeFile, new List<FileStorageUploadRequest>()
        {
            new(Guid.NewGuid(), request.File!, (short) docType)
        });
        if (uploadPath == null) throw new BadRequestException("حدث خطأ أثناء رفع الملف");
        var @event = new CustomerFileUpdatedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new CustomerFileUpdatedEventData()
        {
            OldFileIdentifier = string.IsNullOrWhiteSpace(request.FileId) ? null : Guid.Parse(request.FileId!),
            FileIdentifier = uploadPath.First().Id,
            FileLink = uploadPath.First().Link,
            FileType = docType!,
        })
        {
        };
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