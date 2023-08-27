using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AdminsManagement.FetchAdminById;
public sealed record FetchAdminByIdQuery: IRequest<ContentResponse<FetchAdminByIdQueryResponse>>
{
    public string? Id { get; set; }
}
