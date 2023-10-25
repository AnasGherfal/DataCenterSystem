using Core.Constants;
using Core.Interfaces.Services;
using Core.Wrappers;
using MediatR;

namespace Web.API.Features.AdminsManagement.FetchPermissions
{
    public class FetchPermissionsQueryHandler: IRequestHandler<FetchPermissionsQuery, ListResponse<FetchPermissionsQueryResponse>>
    {
        private readonly IClientService _client;
        private readonly ILogger<FetchPermissionsQueryHandler> _logger;

        public FetchPermissionsQueryHandler(ILogger<FetchPermissionsQueryHandler> logger, IClientService client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<ListResponse<FetchPermissionsQueryResponse>> Handle(FetchPermissionsQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(new TimeSpan(1), cancellationToken);
            var data = Enum.GetValues(typeof(SystemPermissions))
                .Cast<SystemPermissions>()
                .ToList()
                .Select(p => new FetchPermissionsQueryResponse()
                {
                    Id = (long) p,
                    Name = p.Name(),
                }).ToList();
          return new ListResponse<FetchPermissionsQueryResponse>("", data);
        }
    }
}
