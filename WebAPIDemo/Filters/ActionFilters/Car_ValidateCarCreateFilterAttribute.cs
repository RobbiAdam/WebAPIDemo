using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.InteropServices;
using WebAPIDemo.Data;
using WebAPIDemo.Models;
using WebAPIDemo.Models.Repositories;

namespace WebAPIDemo.Filters.ActionFilters
{
    public class Car_ValidateCarCreateFilterAttribute : ActionFilterAttribute
    {
        private readonly ApplicationDBContext db;

        public Car_ValidateCarCreateFilterAttribute(ApplicationDBContext db)
        {
            this.db = db;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var car = context.ActionArguments["car"] as Car;

            if (car == null)
            {
                context.ModelState.AddModelError("Car", "Car is null");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);

            }
            else
            {
                var existingCar = db.Cars.FirstOrDefault(x =>
                    !string.IsNullOrWhiteSpace(car.CarBrand) && !string.IsNullOrWhiteSpace(x.CarBrand) &&
                    x.CarBrand.ToLower() == car.CarBrand.ToLower() &&
                    !string.IsNullOrWhiteSpace(car.CarName) && !string.IsNullOrWhiteSpace(x.CarName) &&
                    x.CarName.ToLower() == car.CarName.ToLower() &&
                    !string.IsNullOrWhiteSpace(car.CarColor) && !string.IsNullOrWhiteSpace(x.CarColor) &&
                    x.CarColor.ToLower() == car.CarColor.ToLower() &&
                    car.CarSeat.HasValue && car.CarSeat.Value == x.CarSeat.Value);

                if (existingCar != null)
                {
                    context.ModelState.AddModelError("Car", "Car already exist");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                    return;
                }
            }
        }
    }
}
