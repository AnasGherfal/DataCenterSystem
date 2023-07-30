using MediatR;
using Shared.Dtos;

namespace Web.API.Features.CustomerManagement.FetchCustomers
{
    public sealed record FetchCustomersQuery(int? PageNumber, int? PageSize)
        :IRequest<PagedResponse<FetchCustomersQueryResponse>>;
   
}
