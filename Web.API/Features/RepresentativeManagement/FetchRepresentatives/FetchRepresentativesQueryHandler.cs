using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Web.API.Features.RepresentativeManagement.FetchRepresentatives;

public sealed record FetchRepresentativesQueryHandler : IRequestHandler<FetchRepresentativesQuery, PagedResponse<FetchRepresentativesQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchRepresentativesQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<FetchRepresentativesQueryResponse>> Handle(FetchRepresentativesQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var query = _dbContext.Representatives
            .Where(p => string.IsNullOrWhiteSpace(request.CustomerId) || p.CustomerId == Guid.Parse(request.CustomerId!));
        var data = await query.Include(p => p.Customer)
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p => new FetchRepresentativesQueryResponse()
            {
                Id = p.Id,
                CustomerName = p.Customer!.Name,
                FirstName = p.FirstName,
                LastName = p.LastName,
                IdentityNo = p.IdentityNo,
                IdentityType = p.IdentityType,
                Email = p.Email,
                PhoneNo = p.PhoneNo,
                Status = p.Status,
                CreatedOn = p.CreatedOn,
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await query.CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchRepresentativesQueryResponse>("", data, count, pageNumber, pageSize);
    }
}