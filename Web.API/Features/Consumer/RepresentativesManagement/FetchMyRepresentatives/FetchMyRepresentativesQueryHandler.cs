using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentatives;

public sealed record FetchMyRepresentativesQueryHandler : IRequestHandler<FetchMyRepresentativesQuery, PagedResponse<FetchMyRepresentativesQueryResponse>>
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _client;

    public FetchMyRepresentativesQueryHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<PagedResponse<FetchMyRepresentativesQueryResponse>> Handle(FetchMyRepresentativesQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var query = _dbContext.Representatives
            .Where(p => p.CustomerId == _client.GetIdentifier());
        var data = await query.Include(p => p.Customer)
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p => new FetchMyRepresentativesQueryResponse()
            {
                Id = p.Id,
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
        return new PagedResponse<FetchMyRepresentativesQueryResponse>("", data, count, pageNumber, pageSize);
    }
}