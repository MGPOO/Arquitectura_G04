using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConUni_Rest_WebClient.Controllers;

public class AccountController : Controller
{
    [HttpGet, AllowAnonymous]
    public IActionResult Login() => View();

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Login(string user, string pass)
    {
        if (string.Equals(user, "MONSTER", StringComparison.Ordinal) && pass == "monster9")
        {
            var claims = new List<Claim> { new(ClaimTypes.Name, "MONSTER") };
            var id = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
            return RedirectToAction("Index", "Conversor");
        }

        ViewBag.Error = "Credenciales inválidas.";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }
}
