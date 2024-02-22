using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.Repositories;

namespace WebApp.Controllers
{
    public class CarsController : BaseController
    {
        private string _relativeUrl = "cars";
        private readonly IWebApiExecute webApiExecute;

        public CarsController(IWebApiExecute webApiExecute)
        {
            this.webApiExecute = webApiExecute;
        }
        public async Task<IActionResult> Index()
        {
            return View(await webApiExecute.InvokeGet<List<Car>>(_relativeUrl));
        }
        public IActionResult CreateCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await webApiExecute.InvokePost("cars", car);
                    if (response != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (WebApiException ex)
                {
                    HandleWebApiException(ex);
                }

            }
            return View(car);
        }

        public async Task<IActionResult> UpdateCar(int carId)
        {
            try
            {
                var car = await webApiExecute.InvokeGet<Car>($"cars/{carId}");
                if (car != null)
                {
                    return View(car);
                }

            }
            catch (WebApiException ex)
            {
                HandleWebApiException(ex);
                return View();
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(Car car)
        {
            if (ModelState.IsValid)
            {
                await webApiExecute.InvokePut($"cars/{car.CarId}", car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public async Task<IActionResult> DeleteCar(int carId)
        {
            try
            {
                await webApiExecute.InvokeDelete($"cars/{carId}");
                return RedirectToAction(nameof(Index));
            }
            catch (WebApiException ex)
            {
                HandleWebApiException(ex);
                return View(nameof(Index), await webApiExecute.InvokeGet<List<Car>>(_relativeUrl));
            }

        }
    }
}
