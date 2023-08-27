using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionFileById;
public sealed record FetchSubscriptionFileByIdQuery: IRequest<FileStreamResult>
{
    public string? SubscriptionId { get; set; }
    public string? FileId { get; set; }
}
