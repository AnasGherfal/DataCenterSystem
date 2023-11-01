using Core.Constants;
using Core.Entities;
using Core.Events.Invoice;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.InvoiceManagement.MarkInvoiceAsPaid;

public sealed record MarkInvoiceAsPaidCommandHandler : IRequestHandler<MarkInvoiceAsPaidCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly UserManager<Account> _userManager;
    private readonly AppDbContext _dbContext;

    public MarkInvoiceAsPaidCommandHandler(AppDbContext dbContext, IClientService client, UserManager<Account> userManager)
    {
        _dbContext = dbContext;
        _client = client;
        _userManager = userManager;
    }

    public async Task<MessageResponse> Handle(MarkInvoiceAsPaidCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == _client.Email, cancellationToken);
        if (user == null) throw new NotFoundException("USER_NOT_EXIST");
        var result = await _userManager.CheckPasswordAsync(user, request.AdminPassword!);
        if (!result) throw new BadRequestException("INCORRECT_CREDENTIALS_COMBINATION");
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Invoices.SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("INVOICE_NOT_FOUND");
        if (data.Status != InvoiceStatus.Pending) throw new BadRequestException("INVOICE_NOT_PENDING_PAYMENT");
        var @event = new InvoicePaidEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new InvoicePaidEventData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "INVOICE_PAID_SUCCESSFULLY",
        };
    }
}