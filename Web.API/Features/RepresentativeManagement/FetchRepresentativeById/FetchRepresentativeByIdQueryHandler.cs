using Core.Dtos;
using Core.Exceptions;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.RepresentativeManagement.FetchRepresentativeById;

public sealed record FetchRepresentativeByIdQueryHandler : IRequestHandler<FetchRepresentativeByIdQuery, ContentResponse<FetchRepresentativeByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchRepresentativeByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchRepresentativeByIdQueryResponse>> Handle(FetchRepresentativeByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Representatives
            .Include(p => p.Customer)
            .Include(p => p.Documents)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("REPRESENTATIVE_NOT_FOUND");
        var document = data.Documents.FirstOrDefault(p => p.IsActive);
        return new ContentResponse<FetchRepresentativeByIdQueryResponse>("", new FetchRepresentativeByIdQueryResponse()
        {
            Id = data.Id,
            CustomerName = data.Customer!.Name,
            FirstName = data.FirstName,
            LastName = data.LastName,
            IdentityNo = data.IdentityNo,
            IdentityType = data.IdentityType,
            Email = data.Email,
            PhoneNo = data.PhoneNo,
            Status = data.Status,
            CreatedOn = data.CreatedOn,
            Files = data.Documents
                .Select(p => new FetchRepresentativeByIdQueryResponseItem()
                {
                    Id = p.Id,
                    FileType = p.FileType,
                    IsActive = p.IsActive,
                    CreatedOn = p.CreatedOn, 
                }).ToList(),
        });
    }
}