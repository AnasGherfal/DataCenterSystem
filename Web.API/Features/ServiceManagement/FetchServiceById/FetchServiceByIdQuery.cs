using MediatR;
using Shared.Dtos;

namespace Web.API.Features.ServiceManagement.FetchServiceById;
public sealed record FetchServiceByIdQuery(string? Id) 
    : IRequest<ContentResponse<FetchServiceByIdQueryResponse>>;
