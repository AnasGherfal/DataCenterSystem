using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure;
using Infrastructure.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.CustomerManagement.FetchCustomerById;

public sealed record FetchCustomerByIdQueryHandler : IRequestHandler<FetchCustomerByIdQuery, ContentResponse<FetchCustomerByIdQueryResponse>>
{
    private readonly DataCenterContext _dbContext;


    public FetchCustomerByIdQueryHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;

    }
    public async Task<ContentResponse<FetchCustomerByIdQueryResponse>> Handle(FetchCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Customers.FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.id!) && p.Status != GeneralStatus.Deleted, cancellationToken: cancellationToken)
                                       ?? throw new NotFoundException("عذرًا لا وجود لعميل بهذا الرقم يرجى التأكد!");
        return new ContentResponse<FetchCustomerByIdQueryResponse>("", new FetchCustomerByIdQueryResponse
        {
            Id = data.Id,
            Name = data.Name,
            Address = data.Address,
            Email = data.Email,
            PrimaryPhone = data.PrimaryPhone,
            SecondaryPhone = data.SecondaryPhone,
            Status = data.Status,
            Subsicrptions = data.Subscriptions.Select(p => p.Id).ToList(),
            Representative = data.Representatives.Select(p => p.Id).ToList(),
            FileName = data.Files.Select(p => p.Filename).ToList()
        });
    }
}
