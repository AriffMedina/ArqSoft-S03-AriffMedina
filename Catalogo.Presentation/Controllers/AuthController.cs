using CatalogoApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApp.Presentation.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        // ──────────────────────────────────────────────
        //  REGISTRO
        // ──────────────────────────────────────────────

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(string nombreUsuario, string email, string password, string confirmarPassword)
        {
            if (password != confirmarPassword)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View();
            }

            var error = _userService.Registrar(nombreUsuario, email, password);
            if (error != null)
            {
                ViewBag.Error = error;
                return View();
            }

            // Registro exitoso → redirigir al login
            TempData["Mensaje"] = "¡Cuenta creada! Inicia sesión.";
            return RedirectToAction("Login");
        }

        // ──────────────────────────────────────────────
        //  LOGIN
        // ──────────────────────────────────────────────

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var usuario = _userService.ValidarLogin(email, password);
            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View();
            }

            // Guardar sesión
            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioNombre", usuario.NombreUsuario);

            return RedirectToAction("Index", "Home");
        }

        // ──────────────────────────────────────────────
        //  LOGOUT
        // ──────────────────────────────────────────────

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}