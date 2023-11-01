using Core.Constants;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Authentication.Profile;

public sealed record FetchProfileQueryHandler : IRequestHandler<FetchProfileQuery, ContentResponse<FetchProfileQueryResponse>>
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _client;

    public FetchProfileQueryHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<ContentResponse<FetchProfileQueryResponse>> Handle(FetchProfileQuery request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == _client.GetIdentifier(), cancellationToken);
        if (profile == null) throw new NotFoundException("PROFILE_NOT_FOUND");
        return new ContentResponse<FetchProfileQueryResponse>("", new FetchProfileQueryResponse()
        {
            Id = profile.Id,
            EmpId = 0,
            DisplayName = profile.DisplayName,
            Email = profile.Email ?? "",
            CreatedOn = profile.CreatedOn,
            Permissions = 0,
        });
    }
}