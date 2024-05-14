using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using viewbag.Models;

namespace viewbag.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        private readonly AdreinContext context;

        public HomeController(ILogger<HomeController> logger ,AdreinContext context)
        {
            _logger = logger;
            
            this.context = context;
        }

        public IActionResult Index()
        {
            var data = "lorem ainj kmuin riok kulh njkg uiou kjkh";
            ViewBag.lorem = data;
            ViewBag.heading = "Welcome";
            string[] arr = { "name1", "name2", "name3"};
            ViewBag.array = arr;
            return View();
        }
        public IActionResult signup()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}