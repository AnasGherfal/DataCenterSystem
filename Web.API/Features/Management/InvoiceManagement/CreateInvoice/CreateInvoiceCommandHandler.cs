using Core.Entities;
using Core.Events.Invoice;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.InvoiceManagement.CreateInvoice;

public sealed record CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public CreateInvoiceCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var customerId = Guid.Parse(request.CustomerId!);
        var visits = await _dbContext.Visits
            .Where(p => p.CustomerId == customerId 
                        && p.InvoiceId == null 
                        && p.StartTime >= request.IncludeVisitsFrom 
                        && p.EndTime <= request.IncludeVisitsTo)
            .ToListAsync(cancellationToken: cancellationToken);
        if (visits.Count == 0) throw new BadRequestException("NO_VISITS_FOUND");
        var @event = new InvoiceCreatedEvent(_client.GetIdentifier(), Guid.NewGuid(), new InvoiceCreatedEventData()
        {
            CustomerId = customerId,
            IncludeVisitsFrom = request.IncludeVisitsFrom!.Value,
            IncludeVisitsTo = request.IncludeVisitsTo!.Value,
            VisitsId = visits.Select(p => p.Id).ToList(),
            Price = visits.Sum(p => p.VisitPrice),
            Notes = request.Notes ?? "",
        });
        var data = new Invoice();
        data.Apply(@event);
        foreach (var visit in visits)
        {
            visit.Apply(@event);
            _dbContext.Entry(visit).State = EntityState.Modified;
        }
        await _dbContext.Invoices.AddAsync(data, cancellationToken);
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = $"INVOICE_CREATED_SUCCESSFULLY_WITH_{visits.Count}_VISITS",
        };
    }
}