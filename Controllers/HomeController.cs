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

            var asd = accessor.HttpContext.Session.GetString("sessionName");
            if (accessor.HttpContext.Session.GetString("sessionName") != null)
            {
                ViewBag.sessionName = asd;
                if (accessor.HttpContext.Session.GetString("SessionRole") == "Customer")
                {

                    return View();
                }
                else if (accessor.HttpContext.Session.GetString("SessionRole") == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                return View("Login");
            }
            return View();
        }
        public IActionResult product()
        {
            return View();
        }
        [HttpPost]
        public IActionResult product(Product product,IFormFile img)
        {
            var check = product;
            var check2 = img;
            var check3 = "sds";
            return View();
        }
        public IActionResult productlist()
        {
            var show = context.Products.ToList();
            return View(show);
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

            if (show != null)
            {

                var userRole = context.Roles.FirstOrDefault(options => options.RoleId == show.RoleId);
                accessor.HttpContext.Session.SetString("sessionName", show.Name);
                accessor.HttpContext.Session.SetString("sessionEmail", show.Email);
                accessor.HttpContext.Session.SetString("sessionRole", userRole.RoleName);
                if (userRole.RoleName == "Customer")
                {
                    return RedirectToAction("Index");
                }
                else if (userRole.RoleName == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                ViewBag.failed = "Login Failed";
            }
            return View();
        }
        public IActionResult Privacy()
        {
            var asd = accessor.HttpContext.Session.GetString("sessionName");
            if (accessor.HttpContext.Session.GetString("sessionName") != null)
            {




                ViewBag.sessionName = asd;
                if (accessor.HttpContext.Session.GetString("SessionRole") == "Customer")
                {

                    return View();
                }
                else if (accessor.HttpContext.Session.GetString("SessionRole") == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                return View("Login");
            }
            return View();
           
        }
        public IActionResult logout()
        {
            if(accessor.HttpContext.Session.GetString("sessionName") != null)
            {
                accessor.HttpContext.Session.Clear();
                return RedirectToAction("login");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}