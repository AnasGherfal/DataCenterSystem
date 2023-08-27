using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Web.API.Features.InvoiceManagement.FetchInvoices;

public sealed record FetchInvoicesQueryHandler : IRequestHandler<FetchInvoicesQuery, PagedResponse<FetchInvoicesQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchInvoicesQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<FetchInvoicesQueryResponse>> Handle(FetchInvoicesQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var query = _dbContext.Invoices
            .Where(p => string.IsNullOrWhiteSpace(request.CustomerId) || p.CustomerId == Guid.Parse(request.CustomerId!));
        var data = await query.Include(p => p.Customer)
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p => new FetchInvoicesQueryResponse()
            {
                Id = p.Id,
                Customer = p.Customer!.Name,
                VisitsFrom = p.From,
                VisitsTo = p.To,
                Price = p.Price,
                Status = p.Status,
                CreatedOn = p.CreatedOn,
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await query.CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchInvoicesQueryResponse>("", data, count, pageNumber, pageSize);
    }
}