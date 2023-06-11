using Microsoft.AspNetCore.Mvc;

namespace HotelProject.WebApi.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
