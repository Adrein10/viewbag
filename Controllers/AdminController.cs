using Microsoft.AspNetCore.Mvc;

namespace viewbag.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
