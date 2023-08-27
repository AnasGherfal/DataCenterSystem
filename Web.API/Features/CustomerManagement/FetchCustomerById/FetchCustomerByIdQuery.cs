using MediatR;
using Shared.Dtos;

namespace Web.API.Features.CustomerManagement.FetchCustomerById;

public sealed record FetchCustomerByIdQuery: IRequest<ContentResponse<FetchCustomerByIdQueryResponse>>
{
    public string? Id { get; set; }
}
