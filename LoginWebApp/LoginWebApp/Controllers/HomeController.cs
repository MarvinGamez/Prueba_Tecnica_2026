using Microsoft.AspNetCore.Mvc;
using LoginWebApp.Filters;

namespace LoginWebApp.Controllers
{
    [AuthFilter]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.User = HttpContext.Session.GetString("FullName");
            return View();
        }
    }
}
