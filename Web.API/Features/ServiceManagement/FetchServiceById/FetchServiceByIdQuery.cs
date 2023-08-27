using MediatR;
using Shared.Dtos;

namespace Web.API.Features.ServiceManagement.FetchServiceById;
public sealed record FetchServiceByIdQuery: IRequest<ContentResponse<FetchServiceByIdQueryResponse>>
{
    public string? Id { get; set; }
}
