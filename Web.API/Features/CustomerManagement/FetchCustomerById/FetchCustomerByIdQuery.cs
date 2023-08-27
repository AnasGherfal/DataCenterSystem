using Core.Wrappers;
using MediatR;

namespace Web.API.Features.CustomerManagement.FetchCustomerById;

public sealed record FetchCustomerByIdQuery: IRequest<ContentResponse<FetchCustomerByIdQueryResponse>>
{
    public string? Id { get; set; }
}
