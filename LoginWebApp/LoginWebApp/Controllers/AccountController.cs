using LoginWebApp.BLL;
using LoginWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LoginWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _auth;

        public AccountController(AuthService auth)
        {
            _auth = auth;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLogin model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _auth.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                ModelState.Clear(); 
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("FullName", user.FullName);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegister model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _auth.Register(model);
                TempData["Success"] = "Cuenta creada correctamente";
                return RedirectToAction("Login");
            }
            catch (SqlException ex)
            {
                ViewBag.Error = ex.Message.Contains("Username")
                    ? "El nombre de usuario ya existe"
                    : "Error al crear usuario";

                return View(model);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
