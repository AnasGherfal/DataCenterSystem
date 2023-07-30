using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Web.API.Features.AdminsManagement.FetchAdmins;

namespace Web.API.Features.CustomerManagement.FetchCustomers;

public sealed record FetchCustomersQueryHandler : IRequestHandler<FetchCustomersQuery,PagedResponse<FetchCustomersQueryResponse>>
{
    private readonly DataCenterContext _dbContext;


    public FetchCustomersQueryHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<FetchCustomersQueryResponse>> Handle(FetchCustomersQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var data = await _dbContext.Customers
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p => new FetchCustomersQueryResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Email = p.Email,
                PrimaryPhone = p.PrimaryPhone,
                SecondaryPhone = p.SecondaryPhone,
                Status = p.Status,
                Subsicrptions = p.Subscriptions.Select(p => p.Id).ToList(),
                Representative = p.Representatives.Select(p => p.Id).ToList(),
                FileName = p.Files.Select(p => p.Filename).ToList()
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await _dbContext.Customers.CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchCustomersQueryResponse>("", data, count, pageNumber, pageSize);
    }
}
