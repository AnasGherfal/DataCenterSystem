using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptionFileById;
public sealed record FetchMySubscriptionFileByIdQuery: IRequest<FileStreamResult>
{
    public string? SubscriptionId { get; set; }
    public string? FileId { get; set; }
}
