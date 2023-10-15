using Core.Constants;
using Core.Entities;
using Core.Events.Abstracts;
using Core.Events.Service;
using Core.Exceptions;
using Core.Helpers;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Web.API.Features.AuditManagement.FetchProjectionById;

public sealed record FetchProjectionByIdQueryHandler : IRequestHandler<FetchProjectionByIdQuery, ContentResponse<FetchProjectionByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchProjectionByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchProjectionByIdQueryResponse>> Handle(FetchProjectionByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Events
            .Where(p => p.AggregateId == Guid.Parse(request.Id!)
            && (request.Date == null || p.DateTime <= request.Date.Value))
            .OrderBy(p => p.Sequence)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);
        if (data.Count == 0) throw new NotFoundException("NO_DATA_FOUND");
        var response = new FetchProjectionByIdQueryResponse()
        {
            Projection = request.ProjectionType!.Value,
            Type = data.Last().Type.KeyName(),
            OccurredOn = data.Last().DateTime,
            OccuredBy = data.Last().UserId.ToString(),
            Before = "",
            After = "",
        };
        switch (request.ProjectionType!.Value)
        {
            case EntityType.Admin:
                response.Before = Before<Admin>(data);
                response.After = After<Admin>(data);
                break;
            case EntityType.Customer:
                response.Before = Before<Customer>(data);
                response.After = After<Customer>(data);
                break;
            case EntityType.Invoice:
                response.Before = Before<Invoice>(data);
                response.After = After<Invoice>(data);
                break;
            case EntityType.Representative:
                response.Before = Before<Representative>(data);
                response.After = After<Representative>(data);
                break;
            case EntityType.Service:
                response.Before = Before<Service>(data);
                response.After = After<Service>(data);
                break;
            case EntityType.Subscription:
                response.Before = Before<Subscription>(data);
                response.After = After<Subscription>(data);
                break;
            case EntityType.TimeShift:
                response.Before = Before<TimeShift>(data);
                response.After = After<TimeShift>(data);
                break;
            case EntityType.Visit:
                response.Before = Before<Visit>(data);
                response.After = After<Visit>(data);
                break;
            default:
                throw new BadRequestException("UNKNOWN_PROJECTION_TYPE");
        }
        return new ContentResponse<FetchProjectionByIdQueryResponse>("", response);
    }

    private string Before<T>(IList<Event> events) 
        => JsonConvert.SerializeObject(ToResponseObject(BuildFromEvent<T>(events)));
    private string After<T>(ICollection<Event> events) 
        => events.Count > 1 ? JsonConvert.SerializeObject(ToResponseObject(BuildFromEvent<T>(events.Take(events.Count - 1).ToList()))) : "";

    private static dynamic ToResponseObject<T>(T obj)
    {
        return obj switch
        {
            Admin data => new
            {
                data.Id,
                data.DisplayName,
                Email = data.Email ?? "",
                data.Permissions,
                IsActive = data.Enabled,
                data.CreatedOn,
                data.UpdatedOn,
            },
            Customer data => new
            {
                data.Id,
                data.Name,
                data.Address,
                data.City,
                data.PrimaryPhone,
                data.SecondaryPhone,
                data.Email,
                data.Status,
                data.CreatedOn,
                Files = data.Documents
                    .Select(p => new
                    {
                        p.Id,
                        p.FileType,
                        p.IsActive,
                        p.CreatedOn, 
                    }).ToList(),
            },
            Invoice data => new
            {
                data.Id,
                VisitsFrom = data.From,
                VisitsTo = data.To,
                data.Price,
                data.Status,
                data.Notes,
                data.CreatedOn,
                NumberOfVisits = data.Visits.Count,
            },
            Representative data => new
            {
                data.Id,
                data.FirstName,
                data.LastName,
                data.IdentityNo,
                data.IdentityType,
                data.Email,
                data.PhoneNo,
                data.Status,
                data.CreatedOn,
                Files = data.Documents
                    .Select(p => new
                    {
                        p.Id,
                        p.FileType,
                        p.IsActive,
                        p.CreatedOn, 
                    }).ToList(),
            },
            Service data => new
            {
                data.Id,
                data.Name,
                data.AmountOfPower,
                data.AcpPort,
                data.Dns,
                data.MonthlyVisits,
                data.Price,
                data.Status,
                data.CreatedOn,
            },
            Subscription data => new
            {
                data.Id,
                data.StartDate,
                data.EndDate,
                data.TotalPrice,
                data.Status,
                data.CreatedOn,
                Files = data.Documents
                    .Select(p => new
                    {
                        p.Id,
                        p.FileType,
                        p.IsActive,
                        p.CreatedOn, 
                    }).ToList(),
            },
            TimeShift data => new
            {
                data.Id,
                data.Date,
                data.Day,
                data.StartTime,
                data.EndTime,
                data.PriceForFirstHour,
                data.PriceForRemainingHours,
                data.CreatedOn,
            },
            Visit data => new
            {
                data.Id,
                data.ExpectedStartTime,
                data.ExpectedEndTime,
                data.StartTime,
                data.EndTime,
                data.TotalTime,
                data.VisitPrice,
                data.Notes,
                data.VisitType,
                IsPaid = data.InvoiceId != null,
                data.CreatedOn,
                Companions = data.Companions
                    .Select(p => new
                    {
                        p.FirstName,
                        p.LastName,
                        p.IdentityNo,
                        p.IdentityType,
                        p.JobTitle,
                    }).ToList(),
                NumberOfRepresentatives = data.Representatives.Count,
            },
            _ => throw new BadRequestException("UNKNOWN_PROJECTION_TYPE")
        };
    }
    private static T BuildFromEvent<T>(IList<Event> events)
    {
        try
        {
            if (events.Count == 0) throw new NotFoundException("NO_DATA_FOUND");
            var projection = (T) Activator.CreateInstance(typeof(T))!;
            long expectedSequence = 1;
            foreach (var e in events.OrderBy(p => p.Sequence))
            {
                if (e.Sequence != expectedSequence) throw new FlowException(nameof(events), "Sequence is not expected");
                var applyMethod = projection.GetType().GetMethod("Apply", new[] { e.GetType() });
                applyMethod.Invoke(projection, new object[] { e });
                expectedSequence++;
            }
            return projection;
        } 
        catch (Exception ex)
        {
            switch (ex)
            {
                case RuntimeBinderException:
                    throw new BadRequestException("ID_NOT_MATCH_WITH_TYPE");
                default:
                    throw;
            }
        }
    }
}