using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Models;

namespace WebApi.Filter
{
    public class CustomValidatorModelState : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var fiedValidatorViewModel = new FieldValidatorViewModelOutput(context.ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage));
                context.Result = new BadRequestObjectResult(fiedValidatorViewModel);
            }
        }
    }
}