using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Data;
using WebAPIDemo.Filters;
using WebAPIDemo.Filters.ActionFilters;
using WebAPIDemo.Filters.ExceptionFilters;
using WebAPIDemo.Models;
using WebAPIDemo.Models.Repositories;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CarsController : ControllerBase
    {
        private readonly ApplicationDBContext db;

        public CarsController(ApplicationDBContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetCar()
        {
            return Ok(db.Cars.ToList());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(Car_ValidateCarIdFilterAttribute))]
        public IActionResult GetCarByID(int id)
        {
            return Ok(HttpContext.Items["car"]);
        }

        [HttpPost]
        [TypeFilter(typeof(Car_ValidateCarCreateFilterAttribute))]
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
        public IActionResult UpdateCar(int id, Car car)
        {
            var carToUpdate = HttpContext.Items["car"] as Car;

            carToUpdate.CarBrand = car.CarBrand;
            carToUpdate.CarName = car.CarName;
            carToUpdate.CarSeat = car.CarSeat;
            carToUpdate.CarType = car.CarType;
            carToUpdate.CarColor = car.CarColor;
            carToUpdate.CarPrice = car.CarPrice;

            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(Car_ValidateCarIdFilterAttribute))]
        public IActionResult DeleteCar(int id)
        {
            var carToDelete = HttpContext.Items["car"] as Car;

            db.Cars.Remove(carToDelete);
            db.SaveChanges();

            return Ok(carToDelete);
        }
    }
}
