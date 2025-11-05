using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConUni_Soap_Dotnet_CliWeb_G04.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("~/Views/Login/Index.cshtml");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
        {
            // Quitar espacios
            username = username?.Trim();
            password = password?.Trim();

            // ========== VALIDACIONES ==========
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.Error = "El usuario es obligatorio.";
                return View("~/Views/Login/Index.cshtml");
            }

            if (string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "La contraseña es obligatoria.";
                return View("~/Views/Login/Index.cshtml");
            }

            // Validar longitud mínima
            if (username.Length < 3)
            {
                ViewBag.Error = "El usuario debe tener al menos 3 caracteres.";
                return View("~/Views/Login/Index.cshtml");
            }

            // Validar caracteres permitidos: letras, números, . _ -
            if (!Regex.IsMatch(username, @"^[A-Za-z0-9._-]+$"))
            {
                ViewBag.Error = "El usuario solo puede contener letras, números, punto, guion y guion bajo.";
                return View("~/Views/Login/Index.cshtml");
            }

            // Validar contraseña con una letra y un número mínimo
            if (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).+$"))
            {
                ViewBag.Error = "La contraseña debe contener al menos una letra y un número.";
                return View("~/Views/Login/Index.cshtml");
            }

            // ========== CREDENCIALES REALES ==========
            const string USER = "MONSTER";
            const string PASS = "monster9";

            if (username == USER && password == PASS)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, USER),
                    new Claim(ClaimTypes.Role, "User")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Conversor");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View("~/Views/Login/Index.cshtml");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
