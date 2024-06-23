using Microsoft.AspNetCore.Mvc;

namespace MilitaryASPWeb.Controllers
{
    public class HomeController : Controller
    {
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
