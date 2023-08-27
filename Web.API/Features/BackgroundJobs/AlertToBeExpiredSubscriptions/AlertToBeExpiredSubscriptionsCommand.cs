using MediatR;

namespace Web.API.Features.BackgroundJobs.AlertToBeExpiredSubscriptions;
public sealed record AlertToBeExpiredSubscriptionsCommand: IRequest<bool>
{
}
