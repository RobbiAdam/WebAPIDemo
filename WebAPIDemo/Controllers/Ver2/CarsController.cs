using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Attributes;
using WebAPIDemo.Data;
using WebAPIDemo.Filters;
using WebAPIDemo.Filters.ActionFilters;
using WebAPIDemo.Filters.ActionFilters.Ver2;
using WebAPIDemo.Filters.AuthFilters;
using WebAPIDemo.Filters.ExceptionFilters;
using WebAPIDemo.Models;
using WebAPIDemo.Models.Repositories;

namespace WebAPIDemo.Controllers.Ver2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{v:apiversion}/[controller]")]
    [JWTTokenAuthFilter]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDBContext db;

        public CarsController(ApplicationDBContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [RequiredClaim("read", "true")]
        public IActionResult GetCar()
        {
            return Ok(db.Cars.ToList());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(Car_ValidateCarIdFilterAttribute))]
        [RequiredClaim("read", "true")]
        public IActionResult GetCarByID(int id)
        {
            return Ok(HttpContext.Items["car"]);
        }

        [HttpPost]
        [TypeFilter(typeof(Car_ValidateCarCreateFilterAttribute))]
        [Car_EnsureCarDescriptionPresentFilter]
        [RequiredClaim("write", "true")]
        public IActionResult CreateCar([FromBody] Car car)
        {
            this.db.Cars.Add(car);
            this.db.SaveChanges();

            return CreatedAtAction(nameof(GetCarByID), new { id = car.CarId }, car);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(Car_ValidateCarIdFilterAttribute))]
        [Car_ValidateCarUpdateFilter]
        [TypeFilter(typeof(Car_HandleUpdateExceptionFilterAttribute))]
        [Car_EnsureCarDescriptionPresentFilter]
        [RequiredClaim("write", "true")]
        public IActionResult UpdateCar(int id, Car car)
        {
            var carToUpdate = HttpContext.Items["car"] as Car;

            carToUpdate.CarBrand = car.CarBrand;
            carToUpdate.CarName = car.CarName;
            carToUpdate.CarSeat = car.CarSeat;
            carToUpdate.CarType = car.CarType;
            carToUpdate.CarColor = car.CarColor;
            carToUpdate.CarPrice = car.CarPrice;
            carToUpdate.CarDescription = car.CarDescription;

            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(Car_ValidateCarIdFilterAttribute))]
        [RequiredClaim("delete", "true")]
        public IActionResult DeleteCar(int id)
        {
            var carToDelete = HttpContext.Items["car"] as Car;

            db.Cars.Remove(carToDelete);
            db.SaveChanges();

            return Ok(carToDelete);
        }
    }
}
