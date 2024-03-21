using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Cookie_Auth.Controllers;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Login(string username)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username)
        };
        
        if (username == "hermano")
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }
        
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
            IsPersistent = true
        };

        HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public IActionResult Authorized()
    {
        return View();
    }
    
    [Authorize(Roles = "Admin")]
    public IActionResult Hidden()
    {
        return View();
    }
}