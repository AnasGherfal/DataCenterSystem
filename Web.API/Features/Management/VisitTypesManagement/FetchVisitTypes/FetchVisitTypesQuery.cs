using Core.Wrappers;
using MediatR;

namespace Web.API.Features.VisitTypesManagement.FetchVisitTypes
{
    public record FetchVisitTypesQuery : IRequest<ListResponse<FetchVisitTypesQueryResponse>>;
}
