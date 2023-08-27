using Core.Wrappers;
using MediatR;

namespace Web.API.Features.ServiceManagement.FetchServiceById;
public sealed record FetchServiceByIdQuery: IRequest<ContentResponse<FetchServiceByIdQueryResponse>>
{
    public string? Id { get; set; }
}
