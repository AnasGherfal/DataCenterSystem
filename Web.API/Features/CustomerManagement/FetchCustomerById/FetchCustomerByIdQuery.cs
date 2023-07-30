using MediatR;
using Shared.Dtos;

namespace Web.API.Features.CustomerManagement.FetchCustomerById;

public sealed record FetchCustomerByIdQuery(string? id)
    :IRequest<ContentResponse<FetchCustomerByIdQueryResponse>>;

