using Core.Entities;
using Core.Exceptions;
using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.CustomerManagement.FetchCustomerById;

public sealed record FetchCustomerByIdQueryHandler : IRequestHandler<FetchCustomerByIdQuery, ContentResponse<FetchCustomerByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;
    private readonly UserManager<Customer> _customerManager;

    public FetchCustomerByIdQueryHandler(AppDbContext dbContext, UserManager<Customer> customerManager)
    {
        _dbContext = dbContext;
        _customerManager = customerManager;
    }

    public async Task<ContentResponse<FetchCustomerByIdQueryResponse>> Handle(FetchCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _customerManager.Users
            .Include(p => p.Documents)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("CUSTOMER_NOT_FOUND");
        var document = data.Documents.FirstOrDefault(p => p.IsActive);
        return new ContentResponse<FetchCustomerByIdQueryResponse>("", new FetchCustomerByIdQueryResponse()
        {
            Id = data.Id,
            Name = data.Name,
            Address = data.Address,
            City = data.City,
            PrimaryPhone = data.PrimaryPhone,
            SecondaryPhone = data.SecondaryPhone,
            Email = data.Email,
            Status = data.Status,
            CreatedOn = data.CreatedOn,
            Files = data.Documents
                .Select(p => new FetchCustomerByIdQueryResponseItem()
                {
                    Id = p.Id,
                    FileType = p.FileType,
                    IsActive = p.IsActive,
                    CreatedOn = p.CreatedOn, 
                }).ToList(),
        });
    }
}