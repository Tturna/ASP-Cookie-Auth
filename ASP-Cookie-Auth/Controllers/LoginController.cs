using System.Security.Claims;
using ASP_Cookie_Auth.Data;
using ASP_Cookie_Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Cookie_Auth.Controllers;

public class LoginController : Controller
{
    private ApplicationDbContext _dbContext;
    
    public LoginController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Register(RegisterDataModel registerData)
    {
        
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Login(LoginDataModel loginData)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", loginData);
        }
        
        var user = _dbContext.Users.FirstOrDefault(u =>
            u.Username == loginData.Username && u.Password == loginData.Password);

        if (user == null)
        {
            ModelState.AddModelError(nameof(loginData.Password), "Invalid username or password.");
            return View("Index", loginData);
        }
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, loginData.Username)
        };
        
        if (loginData.Username == "hermano")
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }
        
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
            // IsPersistent = true
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