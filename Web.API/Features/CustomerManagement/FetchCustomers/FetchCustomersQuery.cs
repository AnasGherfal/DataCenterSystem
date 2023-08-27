using MediatR;
using Shared.Dtos;

namespace Web.API.Features.CustomerManagement.FetchCustomers;
public sealed record FetchCustomersQuery : IRequest<PagedResponse<FetchCustomersQueryResponse>>
{
    public string? Search { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}