using Core.Helpers;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Management.AuditManagement.FetchAudits;

public sealed record FetchAuditsQueryHandler : IRequestHandler<FetchAuditsQuery, PagedResponse<FetchAuditsQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchAuditsQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<FetchAuditsQueryResponse>> Handle(FetchAuditsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var users = _dbContext.Users;
        var query = _dbContext.Events
            .Where(p => string.IsNullOrWhiteSpace(request.RecordId)
                        || p.AggregateId == Guid.Parse(request.RecordId!)).Join(users,

            euser => euser.UserId,
            user => user.Id,
            (euser, user) => new
            {
                Id =euser.Id,
                AggregateId=euser.AggregateId,
                Sequence=euser.Sequence,
                Type=euser.Type,
                Version=euser.Version,
                DateTime=euser.DateTime,
                UserId=euser.UserId,
                UserName = user.DisplayName
});
        var data = await query
            .OrderByDescending(p => p.DateTime)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p => new FetchAuditsQueryResponse()
            {
                Type = p.Type.KeyName(),
                OccuredOn = p.DateTime,
                UserId = p.UserId,
                UserName=p.UserName
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await query.CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchAuditsQueryResponse>("", data, count, pageNumber, pageSize);
    }
}