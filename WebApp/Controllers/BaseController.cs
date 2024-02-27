using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected void HandleWebApiException(WebApiException ex)
        {
            if (ex.ErrorResponse != null &&
                ex.ErrorResponse.Errors != null &&
                ex.ErrorResponse.Errors.Count > 0)
            {
                foreach (var error in ex.ErrorResponse.Errors)
                {
                    ModelState.AddModelError(error.Key, string.Join(";", error.Value));
                }
            }
            else if (ex.ErrorResponse != null)
            {
                ModelState.AddModelError("Error", ex.ErrorResponse.Title);
            }
            else
            {
                ModelState.AddModelError("Error", ex.Message);
            }
        }

    }
}
