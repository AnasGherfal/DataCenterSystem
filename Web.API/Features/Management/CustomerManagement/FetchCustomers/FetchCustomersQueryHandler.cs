using Core.Entities;
using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.CustomerManagement.FetchCustomers;

public sealed record FetchCustomersQueryHandler : IRequestHandler<FetchCustomersQuery, PagedResponse<FetchCustomersQueryResponse>>
{
    private readonly AppDbContext _dbContext;
    private readonly UserManager<Customer> _customerManager;

    public FetchCustomersQueryHandler(AppDbContext dbContext, UserManager<Customer> customerManager)
    {
        _dbContext = dbContext;
        _customerManager = customerManager;
    }

    public async Task<PagedResponse<FetchCustomersQueryResponse>> Handle(FetchCustomersQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var query = _customerManager.Users
            .Where(p => string.IsNullOrWhiteSpace(request.Search)
                        || p.Name.Contains(request.Search)
                        || p.Email.Contains(request.Search)
                        || p.PrimaryPhone.Contains(request.Search));
        var data = await query
            .Include(p => p.Documents)
            .Include(p => p.Representatives)
            .Include(p => p.Subscriptions)
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p => new FetchCustomersQueryResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                City = p.City,
                Email = p.Email,
                PrimaryPhone = p.PrimaryPhone,
                SecondaryPhone = p.SecondaryPhone,
                NumberOfFiles = p.Documents.Count,
                NumberOfSubscriptions = p.Subscriptions.Count,
                NumberOfRepresentatives = p.Representatives.Count,
                Status = p.Status,
                CreatedOn = p.CreatedOn,
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await query.CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchCustomersQueryResponse>("", data, count, pageNumber, pageSize);
    }
}