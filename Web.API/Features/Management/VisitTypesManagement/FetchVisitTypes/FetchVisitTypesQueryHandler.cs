using Core.Constants;
using Core.Helpers;
using Core.Interfaces.Services;
using Core.Wrappers;
using MediatR;
using Web.API.Features.AdminsManagement.FetchPermissions;

namespace Web.API.Features.VisitTypesManagement.FetchVisitTypes
{
    public class FetchVisitTypesQueryHandler: IRequestHandler<FetchVisitTypesQuery, ListResponse<FetchVisitTypesQueryResponse>>
    {
        private readonly IClientService _client;
        private readonly ILogger<FetchVisitTypesQueryHandler> _logger;

        public FetchVisitTypesQueryHandler(ILogger<FetchVisitTypesQueryHandler> logger, IClientService client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<ListResponse<FetchVisitTypesQueryResponse>> Handle(FetchVisitTypesQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(new TimeSpan(1), cancellationToken);
            var data = Enum.GetValues(typeof(VisitType))
                .Cast<VisitType>()
                .ToList()
                .Select(p => new FetchVisitTypesQueryResponse()
                {
                    Id = (long) p,
                    Name = p.KeyName(),
                }).ToList();
          return new ListResponse<FetchVisitTypesQueryResponse>("", data);
        }
    }
}
