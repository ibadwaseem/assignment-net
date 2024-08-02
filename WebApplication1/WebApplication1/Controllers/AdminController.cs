using _2302b1TempEmbedding.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
  
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {


           
            return View();

        }
        [Authorize]
        public IActionResult Privacy()
        {
            TempData.Keep("email");

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
         
            return View();
            
        }

        public IActionResult AdminLogout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("adminemail");
            HttpContext.Session.Remove("role");
            return RedirectToAction("Login");

        }
        public IActionResult UserLogout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("useremail");
            HttpContext.Session.Remove("role");
            return RedirectToAction("Login");

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Products prd)
        {
            if (ModelState.IsValid)
            {
                return Content("Data is in correct format.");
            }
            else
            {
                ViewBag.msg = "Data not valid";
                return View();
            }

        }
    }
}
