using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.VisitsManagement.FetchVisitTypes
{
    public record FetchVisitTypesQuery : IRequest<ListResponse<FetchVisitTypesQueryResponse>>;
}
