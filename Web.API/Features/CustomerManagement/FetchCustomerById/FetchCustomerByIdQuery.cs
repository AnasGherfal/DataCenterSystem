using Core.Dtos;
using MediatR;

namespace Web.API.Features.CustomerManagement.FetchCustomerById;

public sealed record FetchCustomerByIdQuery: IRequest<ContentResponse<FetchCustomerByIdQueryResponse>>
{
    public string? Id { get; set; }
}
