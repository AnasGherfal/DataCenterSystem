using Core.Wrappers;
using MediatR;

namespace Web.API.Features.AdminsManagement.FetchAdminById;
public sealed record FetchAdminByIdQuery: IRequest<ContentResponse<FetchAdminByIdQueryResponse>>
{
    public string? Id { get; set; }
}
