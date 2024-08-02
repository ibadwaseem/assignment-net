using _2302b1TempEmbedding.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        // 2302b1dotnetContext db = new _2302b1dotnetContext();
        private readonly _2302b1dotnetContext db;

        [Authorize]
        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product prd)
        {
            if (ModelState.IsValid)
            {

                db.Products.Add(prd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {

                return View();
            }
        }
        [Authorize]

        public IActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product prd1)
        {
            if (ModelState.IsValid)
            {
                db.Products.Update(prd1);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {

                return View();
            }
        }
        [Authorize]

        public IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product prd1)
        {

            db.Products.Remove(prd1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}

