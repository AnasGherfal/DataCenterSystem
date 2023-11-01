using Core.Exceptions;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentativeById;

public sealed record FetchMyRepresentativeByIdQueryHandler : IRequestHandler<FetchMyRepresentativeByIdQuery, ContentResponse<FetchMyRepresentativeByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchMyRepresentativeByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchMyRepresentativeByIdQueryResponse>> Handle(FetchMyRepresentativeByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Representatives
            .Include(p => p.Documents)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("REPRESENTATIVE_NOT_FOUND");
        var document = data.Documents.FirstOrDefault(p => p.IsActive);
        return new ContentResponse<FetchMyRepresentativeByIdQueryResponse>("", new FetchMyRepresentativeByIdQueryResponse()
        {
            Id = data.Id,
            FirstName = data.FirstName,
            LastName = data.LastName,
            IdentityNo = data.IdentityNo,
            IdentityType = data.IdentityType,
            Email = data.Email,
            PhoneNo = data.PhoneNo,
            Status = data.Status,
            CreatedOn = data.CreatedOn,
            Files = data.Documents
                .Select(p => new FetchMyRepresentativeByIdQueryResponseItem()
                {
                    Id = p.Id,
                    FileType = p.FileType,
                    IsActive = p.IsActive,
                    CreatedOn = p.CreatedOn, 
                }).ToList(),
        });
    }
}