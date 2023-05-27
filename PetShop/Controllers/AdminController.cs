using Microsoft.AspNetCore.Mvc;

namespace PetShop.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
