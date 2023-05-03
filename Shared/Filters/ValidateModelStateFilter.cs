using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Exceptions;

namespace Shared.Filters;

public class ValidateModelStateFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        var validationErrors = context.ModelState
            .Keys
            .SelectMany(k => context.ModelState[k]!.Errors)
            .Select(e => e.ErrorMessage)
            .FirstOrDefault();
        throw new ValidationException( new List<string>()
        {
            validationErrors ?? "",
        });
    }
}