using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.ServiceManagement.FetchServiceById;

public sealed record FetchServiceByIdQueryHandler : IRequestHandler<FetchServiceByIdQuery, ContentResponse<FetchServiceByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchServiceByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchServiceByIdQueryResponse>> Handle(FetchServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Services
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("SERVICE_NOT_FOUND");
        return new ContentResponse<FetchServiceByIdQueryResponse>("", new FetchServiceByIdQueryResponse()
        {
            Id = data.Id,
            Name = data.Name,
            AmountOfPower = data.AmountOfPower,
            AcpPort = data.AcpPort,
            Dns = data.Dns,
            MonthlyVisits = data.MonthlyVisits,
            Price = data.Price,
            Status = data.Status,
            CreatedOn = data.CreatedOn,
        });
    }
}