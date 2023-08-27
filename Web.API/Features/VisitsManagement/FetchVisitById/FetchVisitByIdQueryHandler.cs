using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.VisitsManagement.FetchVisitById;

public sealed record FetchVisitByIdQueryHandler : IRequestHandler<FetchVisitByIdQuery, ContentResponse<FetchVisitByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchVisitByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchVisitByIdQueryResponse>> Handle(FetchVisitByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Visits
            .Include(p => p.Customer)
            .Include(p => p.Subscription)
            .ThenInclude(p => p!.Service)
            .Include(p => p.Representatives)
            .ThenInclude(p => p.Representative)
            .Include(p => p.Companions)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("VISIT_NOT_FOUND");
        return new ContentResponse<FetchVisitByIdQueryResponse>("", new FetchVisitByIdQueryResponse()
        {
            Id = data.Id,
            Customer = data.Customer!.Name,
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
                .Select(p => new FetchVisitByIdQueryResponseCompanion()
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    IdentityNo = p.IdentityNo,
                    IdentityType = p.IdentityType,
                    JobTitle = p.JobTitle,
                }).ToList(),
            Representatives = data.Representatives
                .Select(p => new FetchVisitByIdQueryResponseRepresentative()
                {
                    FirstName = p.Representative!.FirstName,
                    LastName = p.Representative.LastName,
                    IdentityNo = p.Representative.IdentityNo,
                    IdentityType = p.Representative.IdentityType,
                }).ToList(),
        });
    }
}