using Microsoft.AspNetCore.Mvc;

namespace MilitaryWeb.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadData()
        {
            RedirectToAction("Index", "Product");
            return Json(new { success = true });
        }
    }
}
