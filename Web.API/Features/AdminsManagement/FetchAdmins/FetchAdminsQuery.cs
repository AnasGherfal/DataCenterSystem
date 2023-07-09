using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AdminsManagement.FetchAdmins;
public sealed record FetchAdminsQuery(int? PageNumber, int? PageSize) 
    : IRequest<PagedResponse<FetchAdminsQueryResponse>>;
