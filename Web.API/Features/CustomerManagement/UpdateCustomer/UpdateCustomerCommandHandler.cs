using Core.Constants;
using Core.Events.Abstracts;
using Core.Events.Customer;
using Core.Events.Representative;
using Core.Exceptions;
using Core.Interfaces.Dtos;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.CustomerManagement.UpdateCustomer;

public sealed record UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;
    private readonly IUploadFileService _uploadFile;

    public UpdateCustomerCommandHandler(AppDbContext dbContext, IClientService client, IUploadFileService uploadFile)
    {
        _dbContext = dbContext;
        _client = client;
        _uploadFile = uploadFile;
    }

    public async Task<MessageResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Customers
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("customer not found");
        var fileRequests = new List<FileStorageUploadRequest>()
        {
            new(Guid.NewGuid(), request.IdentityDocument!, (short) DocumentType.IdentityDocument),
            new(Guid.NewGuid(), request.CompanyDocument!, (short) DocumentType.CompanyDocument)
        };
        var uploadPath = await _uploadFile.UploadFiles(StorageType.CustomerFile, fileRequests);
        if (uploadPath == null) throw new BadRequestException("حدث خطأ أثناء رفع الملف");
        var @event = new CustomerUpdatedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new CustomerUpdatedEventData()
        {
            Name=request.Name,
            City=request.City,
            SecondaryPhone= request.SecondPhoneNo,
            Address = request.Address!,
            Email = request.Email!,
            PrimaryPhone = request.PrimaryPhone!,
            Files = fileRequests.Select(p => new FileStorageData()
            {
                FileIdentifier = p.Id,
                FileType = (DocumentType)p.FileType,
                FileLink = uploadPath.First(q => q.Id == p.Id).Link,
            }).ToList()

        });
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