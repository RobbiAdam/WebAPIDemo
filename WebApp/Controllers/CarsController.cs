using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.Repositories;

namespace WebApp.Controllers
{
    public class CarsController : Controller
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
            return View(car);
        }
    }
}
