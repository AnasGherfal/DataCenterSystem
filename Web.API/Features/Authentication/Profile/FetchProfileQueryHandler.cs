using Core.Constants;
using Core.Helpers;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Authentication.Profile;

public sealed record FetchProfileQueryHandler : IRequestHandler<FetchProfileQuery, ContentResponse<FetchProfileQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchProfileQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchProfileQueryResponse>> Handle(FetchProfileQuery request, CancellationToken cancellationToken)
    {
        return new ContentResponse<FetchProfileQueryResponse>("", new FetchProfileQueryResponse()
        {
            Id = Guid.NewGuid(),
            DisplayName = "N/A",
            Email = "admin@ltt.ly",
            CreatedOn = DateTime.Now,
            Permissions = SystemPermissions.SuperAdmin,
        });
    }
}