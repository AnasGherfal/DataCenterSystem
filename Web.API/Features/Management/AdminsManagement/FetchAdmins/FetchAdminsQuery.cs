using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.AdminsManagement.FetchAdmins;
public sealed record FetchAdminsQuery: IRequest<PagedResponse<FetchAdminsQueryResponse>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
