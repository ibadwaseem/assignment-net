﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(string email, string password)
        {
            bool IsAuthenticated = false;
            bool IsAdmin = false;
            ClaimsIdentity identity = null;

            if (email == "admin@gmail.com" && password == "123")
            {
                identity = new ClaimsIdentity(new[]
                 {
                    new Claim(ClaimTypes.Name ,"Syed"),
                    new Claim(ClaimTypes.Role ,"Admin"),

                }
                , CookieAuthenticationDefaults.AuthenticationScheme);
                IsAuthenticated = true;
                IsAdmin = true;
            }
            else if (email == "user@gmail.com" && password == "123")
            {
                identity = new ClaimsIdentity(new[]
                  {
                    new Claim(ClaimTypes.Name ,"User1"),
                    new Claim(ClaimTypes.Role ,"User"),

                }
                 , CookieAuthenticationDefaults.AuthenticationScheme);
                IsAuthenticated = true;
                IsAdmin = false;

            }

            if (IsAuthenticated && IsAdmin)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Admin");
            }
            else if (IsAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();

            }
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

    }
}
