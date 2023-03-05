using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Dtos;

namespace Shared.Filters;

public class ValidateModelStateFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var validationErrors = context.ModelState
            .Keys
            .SelectMany(k => context.ModelState[k]!.Errors)
            .Select(e => e.ErrorMessage)
            .FirstOrDefault();

        var json = new MessageResponse
        {
            Msg = validationErrors!
        };

        context.Result = new BadRequestObjectResult(json);
    }
}