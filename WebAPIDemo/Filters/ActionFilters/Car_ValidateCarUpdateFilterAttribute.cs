using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using WebAPIDemo.Models;

namespace WebAPIDemo.Filters
{
    public class Car_ValidateCarUpdateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var id = context.ActionArguments["id"] as int?;
            var car = context.ActionArguments["Car"] as Car;

            if (id.HasValue && car != null && id != car.CarId)
            {
                context.ModelState.AddModelError("CarId", "Car id is not the same as id");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            return;
        }
    }
}
