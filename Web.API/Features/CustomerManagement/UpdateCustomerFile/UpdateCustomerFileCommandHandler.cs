using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Events.Customer;
using Infrastructure.Events.Representative;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.ClientService;
using Web.API.Services.UploadService;
using Web.API.Services.UploadService.Dtos;

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
        var data = await _dbContext.Customers
            .Include(p => p.Documents)
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("Customer not found");
        if (data.Status != GeneralStatus.Active) throw new BadRequestException("Sorry, this Customer is not active");
        var uploadPath = await _uploadFile.UploadFiles(StorageType.RepresentativeFile, new List<FileStorageUploadRequest>()
        {
            new(Guid.NewGuid(), request.File!, (short) request.DocType!.Value)
        });
        if (uploadPath == null) throw new BadRequestException("حدث خطأ أثناء رفع الملف");
        var @event = new CustomerFileUpdatedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new CustomerFileUpdatedEventData()
        {
            OldFileIdentifier = string.IsNullOrWhiteSpace(request.FileId) ? null : Guid.Parse(request.FileId!),
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