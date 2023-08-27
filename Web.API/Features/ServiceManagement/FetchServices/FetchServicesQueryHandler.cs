using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Web.API.Features.ServiceManagement.FetchServices;

public sealed record FetchServicesQueryHandler : IRequestHandler<FetchServicesQuery, PagedResponse<FetchServicesQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchServicesQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<FetchServicesQueryResponse>> Handle(FetchServicesQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var data = await _dbContext.Services
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p => new FetchServicesQueryResponse()
            {
                Id = p.Id,
                Name = p.Name,
                AmountOfPower = p.AmountOfPower,
                AcpPort = p.AcpPort,
                Dns = p.Dns,
                MonthlyVisits = p.MonthlyVisits,
                Price = p.Price,
                Status = p.Status,
                CreatedOn = p.CreatedOn,
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await _dbContext.Services.CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchServicesQueryResponse>("", data, count, pageNumber, pageSize);
    }
}