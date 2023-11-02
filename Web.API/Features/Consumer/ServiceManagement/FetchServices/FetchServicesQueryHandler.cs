using Core.Constants;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Consumer.ServiceManagement.FetchServices;

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
            .Where(p => p.Status == GeneralStatus.Active)
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
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await _dbContext.Services.Where(p => p.Status == GeneralStatus.Active).CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchServicesQueryResponse>("", data, count, pageNumber, pageSize);
    }
}