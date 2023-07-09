using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AdminsManagement.FetchAdminById;
public sealed record FetchAdminByIdQuery(string? Id) 
    : IRequest<ContentResponse<FetchAdminByIdQueryResponse>>;
