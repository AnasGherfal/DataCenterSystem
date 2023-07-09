using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.API.DI;

namespace Web.API.Abstracts;

[ApiExplorerSettings(GroupName = SwaggerExtension.ConsumerV1)]
[Route("v1.0/consumer/[controller]")]
public abstract class ConsumerController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
}