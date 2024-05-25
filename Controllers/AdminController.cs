using Microsoft.AspNetCore.Mvc;
using viewbag.Models;

namespace viewbag.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IHttpContextAccessor accessor;
        private readonly AdreinContext context;

        public AdminController(ILogger<HomeController> logger, IHttpContextAccessor accessor, AdreinContext context)
        {
            this.logger = logger;
            this.accessor = accessor;
            this.context = context;
        }
        public IActionResult Index()
        {
            var asd = accessor.HttpContext.Session.GetString("sessionName");
            if (accessor.HttpContext.Session.GetString("sessionName") != null)
            {
                ViewBag.sessionName = asd;
                if (accessor.HttpContext.Session.GetString("SessionRole") == "Admin")
                {

                    return View();
                }
                else if (accessor.HttpContext.Session.GetString("SessionRole") == "Customer")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View("Login");
            }
            return View();
        }
    }
}
