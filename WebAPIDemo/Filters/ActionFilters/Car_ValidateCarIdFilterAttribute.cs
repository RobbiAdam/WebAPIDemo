using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIDemo.Data;
using WebAPIDemo.Models.Repositories;

namespace WebAPIDemo.Filters
{
    public class Car_ValidateCarIdFilterAttribute : ActionFilterAttribute
    {
        private readonly ApplicationDBContext db;

        public Car_ValidateCarIdFilterAttribute(ApplicationDBContext db)
        {
            this.db = db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var carId = context.ActionArguments["id"] as int?;

            if (carId.HasValue)
            {
                if (carId.Value <= 0)
                {
                    context.ModelState.AddModelError("CarId", "Car Id is invalid");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }

                else
                {
                    var car = db.Cars.Find(carId.Value);

                    if (car == null)
                    {
                        context.ModelState.AddModelError("CarId", "Car Id doesn't exist");
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status404NotFound
                        };
                        context.Result = new NotFoundObjectResult(problemDetails);
                    }
                    else
                    {
                        context.HttpContext.Items["car"] = car;
                    }

                }
            }
        }
    }
}
