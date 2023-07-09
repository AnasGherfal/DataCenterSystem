using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.API.DI;

namespace Web.API.Abstracts;

[ApiExplorerSettings(GroupName = SwaggerExtension.ManagementV1)]
[Route("v1.0/management/[controller]")]
public abstract class ManagementController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
}