using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.InvoiceManagement.FetchInvoiceById;

public sealed record FetchInvoiceByIdQueryHandler : IRequestHandler<FetchInvoiceByIdQuery, ContentResponse<FetchInvoiceByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchInvoiceByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchInvoiceByIdQueryResponse>> Handle(FetchInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Invoices
            .Include(p => p.Customer)
            .Include(p => p.Visits)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("INVOICE_NOT_FOUND");
        return new ContentResponse<FetchInvoiceByIdQueryResponse>("", new FetchInvoiceByIdQueryResponse()
        {
            Id = data.Id,
            Customer = data.Customer!.Name,
            VisitsFrom = data.From,
            VisitsTo = data.To,
            Price = data.Price,
            Status = data.Status,
            Notes = data.Notes,
            CreatedOn = data.CreatedOn,
            NumberOfVisits = data.Visits.Count,
        });
    }
}