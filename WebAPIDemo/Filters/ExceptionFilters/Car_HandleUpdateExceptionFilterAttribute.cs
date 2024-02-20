using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIDemo.Models.Repositories;
using WebAPIDemo.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Data;

namespace WebAPIDemo.Filters.ExceptionFilters
{
    public class Car_HandleUpdateExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ApplicationDBContext db;

        public Car_HandleUpdateExceptionFilterAttribute(ApplicationDBContext db)
        {
            this.db = db;
        }
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strCarId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strCarId, out int carId))
            {
                if (db.Cars.FirstOrDefault(x => x.CarId == carId) == null)
                {
                    context.ModelState.AddModelError("CarId", "Car doesn't exist");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
                return;
            }
        }
    }
}
