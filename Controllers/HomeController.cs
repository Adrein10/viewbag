using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
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
        [HttpPost]
        public IActionResult product(Product product, IFormFile img)
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
            if (img != null && img.Length > 1)
            {
                var filetype = System.IO.Path.GetExtension(img.FileName).Substring(1);

                if (filetype == "png" || filetype == "jpeg" || filetype == "jfif")
                {
                    var realname = Path.GetFileName(img.FileName);

                    var name = Guid.NewGuid() + realname;

                    var folder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/images");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    var path = Path.Combine(folder, name);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }

                    var dbPath = Path.Combine("images", name);

                    Product data = new Product()
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        Discription = product.Discription,
                        Image = dbPath
                    };
                    context.Products.Add(data);
                    context.SaveChanges();
                    return RedirectToAction("productlist");
                }
                else
                {
                    ViewBag.failedfile = "File type is not supported";
                }
            }
            return View();
        }
        public IActionResult productlist()
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
            var show = context.Products.ToList();
            return View(show);
        }
        public IActionResult productedit(int id)
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
            var show = context.Products.Find(id);
            return View(show);
        }
        [HttpPost]
        public IActionResult productedit(int id,Product product,IFormFile img)
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
            var finduser = context.Products.FirstOrDefault(options => options.Id == id);
            if (img != null && img.Length > 1)
            {
                var filetype = System.IO.Path.GetExtension(img.FileName).Substring(1);

                if (filetype == "png" || filetype == "jpeg" || filetype == "jfif")
                {
                    var realname = Path.GetFileName(img.FileName);

                    var name = Guid.NewGuid() + realname;

                    var folder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/images");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    var path = Path.Combine(folder, name);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }

                    var dbPath = Path.Combine("images", name);


                    finduser.Name = finduser.Name;
                    finduser.Price = finduser.Price;
                    finduser.Quantity = finduser.Quantity;
                    finduser.Discription = finduser.Discription;
                    finduser.Image = dbPath;


                    context.SaveChanges();
                    return RedirectToAction("productlist");
                }
                else if(finduser.Name != null)
                {
                    finduser.Name = finduser.Name;
                    finduser.Price = finduser.Price;
                    finduser.Quantity = finduser.Quantity;
                    finduser.Discription = finduser.Discription;
                    context.SaveChanges();
                    return RedirectToAction("productlist");
                }
                else if (finduser.Price != null)
                {
                    finduser.Name = finduser.Name;
                    finduser.Price = finduser.Price;
                    finduser.Quantity = finduser.Quantity;
                    finduser.Discription = finduser.Discription;
                    context.SaveChanges();
                    return RedirectToAction("productlist");
                }
                else if (finduser.Quantity != null)
                {
                    finduser.Name = finduser.Name;
                    finduser.Price = finduser.Price;
                    finduser.Quantity = finduser.Quantity;
                    finduser.Discription = finduser.Discription;
                    context.SaveChanges();
                    return RedirectToAction("productlist");
                }
                else if (finduser.Discription != null)
                {
                    finduser.Name = finduser.Name;
                    finduser.Price = finduser.Price;
                    finduser.Quantity = finduser.Quantity;
                    finduser.Discription = finduser.Discription;
                    context.SaveChanges();
                    return RedirectToAction("productlist");
                }
                else
                {
                    ViewBag.failedfile = "File type is not supported";
                }
            }
            return View();
        }
        public IActionResult deleteproduct(int id)
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
            var show = context.Products.Find(id);
            return View(show);
        }
        [HttpPost]
        public IActionResult deleteproduct(int id,Product product)
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
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("productlist");
        }
        public IActionResult signup()
        {
            var asd = accessor.HttpContext.Session.GetString("sessionName");
            if (accessor.HttpContext.Session.GetString("sessionName") != null)
            {
                ViewBag.sessionName = asd;
                if (accessor.HttpContext.Session.GetString("SessionRole") == "Customer")
                {

                    return RedirectToAction("Index", "Home");
                }
                else if (accessor.HttpContext.Session.GetString("SessionRole") == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                return View();
            }
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
            var asd = accessor.HttpContext.Session.GetString("sessionName");
            if (accessor.HttpContext.Session.GetString("sessionName") != null)
            {
                ViewBag.sessionName = asd;
                if (accessor.HttpContext.Session.GetString("SessionRole") == "Customer")
                {

                    return RedirectToAction("Index", "Home");
                }
                else if (accessor.HttpContext.Session.GetString("SessionRole") == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                return View();
            }

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