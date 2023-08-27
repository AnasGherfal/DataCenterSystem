using Core.Dtos;
using MediatR;

namespace Web.API.Features.AdminsManagement.FetchPermissions
{
    public record FetchPermissionsQuery : IRequest<ListResponse<FetchPermissionsQueryResponse>>;
}
