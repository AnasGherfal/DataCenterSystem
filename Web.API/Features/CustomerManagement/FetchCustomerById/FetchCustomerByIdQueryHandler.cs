using Core.Dtos;
using Core.Exceptions;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.CustomerManagement.FetchCustomerById;

public sealed record FetchCustomerByIdQueryHandler : IRequestHandler<FetchCustomerByIdQuery, ContentResponse<FetchCustomerByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchCustomerByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchCustomerByIdQueryResponse>> Handle(FetchCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Customers
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