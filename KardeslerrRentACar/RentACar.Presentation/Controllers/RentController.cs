using Microsoft.AspNetCore.Mvc;

namespace RentACar.Presentation.Controllers
{
    public class RentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
