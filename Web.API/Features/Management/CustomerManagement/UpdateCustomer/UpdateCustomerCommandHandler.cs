using Core.Constants;
using Core.Entities;
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
using Microsoft.AspNetCore.Identity;
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
        var data = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("customer not found");
       
        var @event = new CustomerUpdatedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new CustomerUpdatedEventData()
        {
            Name=request.Name!,
            City=request.City!,
            SecondaryPhone= request.SecondaryPhone!,
            Address = request.Address!,
            Email = request.Email!,
            PrimaryPhone = request.PrimaryPhone!,

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