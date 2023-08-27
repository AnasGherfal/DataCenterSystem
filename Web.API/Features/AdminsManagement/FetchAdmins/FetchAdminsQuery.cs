using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AdminsManagement.FetchAdmins;
public sealed record FetchAdminsQuery: IRequest<PagedResponse<FetchAdminsQueryResponse>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
