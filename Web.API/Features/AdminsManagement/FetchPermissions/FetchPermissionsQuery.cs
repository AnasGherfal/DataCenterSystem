using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AdminsManagement.FetchPermissions
{
    public record FetchPermissionsQuery : IRequest<ListResponse<FetchPermissionsQueryResponse>>;
}
