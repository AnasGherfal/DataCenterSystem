using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Consumer.VisitsManagement.FetchMyVisitById;

public sealed record FetchMyVisitByIdQueryHandler : IRequestHandler<FetchMyVisitByIdQuery, ContentResponse<FetchMyVisitByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _client;

    public FetchMyVisitByIdQueryHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<ContentResponse<FetchMyVisitByIdQueryResponse>> Handle(FetchMyVisitByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Visits
            .Include(p => p.Subscription)
            .ThenInclude(p => p!.Service)
            .Include(p => p.Representatives)
            .ThenInclude(p => p.Representative)
            .Include(p => p.Companions)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!)
                && p.CustomerId == _client.GetIdentifier(), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("VISIT_NOT_FOUND");
        return new ContentResponse<FetchMyVisitByIdQueryResponse>("", new FetchMyVisitByIdQueryResponse()
        {
            Id = data.Id,
            Service = data.Subscription!.Service!.Name,
            ExpectedStartTime = data.ExpectedStartTime,
            ExpectedEndTime = data.ExpectedEndTime,
            StartTime = data.StartTime,
            EndTime = data.EndTime,
            TotalTime = data.TotalTime,
            VisitPrice = data.VisitPrice,
            Notes = data.Notes,
            VisitType = data.VisitType,
            IsPaid = data.InvoiceId != null,
            CreatedOn = data.CreatedOn,
            Companions = data.Companions
                .Select(p => new FetchMyVisitByIdQueryResponseCompanion()
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    IdentityNo = p.IdentityNo,
                    IdentityType = p.IdentityType,
                    JobTitle = p.JobTitle,
                }).ToList(),
            Representatives = data.Representatives
                .Select(p => new FetchMyVisitByIdQueryResponseRepresentative()
                {
                    FirstName = p.Representative!.FirstName,
                    LastName = p.Representative.LastName,
                    IdentityNo = p.Representative.IdentityNo,
                    IdentityType = p.Representative.IdentityType,
                }).ToList(),
        });
    }
}