using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.AdminsManagement.FetchPermissions
{
    public record FetchPermissionsQuery : IRequest<ListResponse<FetchPermissionsQueryResponse>>;
}
