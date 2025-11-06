using Cliente_Rest_G04.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cliente_Rest_G04.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
    {
        // *** CREDENCIALES QUEMADAS ***
        if (string.Equals(vm.UserName, "MONSTER", StringComparison.OrdinalIgnoreCase) &&
            vm.Password == "monster9")
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, "MONSTER")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return Redirect(returnUrl ?? Url.Action("Index", "Conversor")!);
        }

        vm.Error = "Usuario o contraseña incorrectos.";
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }
}
