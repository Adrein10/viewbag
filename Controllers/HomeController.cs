using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using viewbag.Models;

namespace viewbag.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IHttpContextAccessor accessor;
		private readonly AdreinContext context;

        public HomeController(ILogger<HomeController> logger ,IHttpContextAccessor accessor,AdreinContext context)
        {
            _logger = logger;
			this.accessor = accessor;
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
            CustormUser newdata = new CustormUser()
            {
                CUser = new User(),
                CRole = context.Roles.ToList()
            };
            return View(newdata);
        }
        [HttpPost]
        public IActionResult signup(CustormUser custorm)
        {
            User user = new User()
            {
                Name = custorm.CUser.Name,
                Email = custorm.CUser.Email,
                Password = custorm.CUser.Password,
                RoleId = custorm.CUser.RoleId
            };
            context.Users.Add(user);
            context.SaveChanges();
            return RedirectToAction("login");
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(User user)
        {
            var show = context.Users.Where(option => option.Email == user.Email && option.Password == user.Password).FirstOrDefault();
            if(show != null)
            {
                accessor.HttpContext.Session.SetString("session", show.Name);
                return RedirectToAction("Index");
            }else
            {
                ViewBag.failed = "Login Failed";
            }
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