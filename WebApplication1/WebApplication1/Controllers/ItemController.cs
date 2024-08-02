using _2302b1TempEmbedding.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class ItemController : Controller
    {
        //_2302b1dotnetContext db = new _2302b1dotnetContext();

        private readonly _2302b1dotnetContext db;
        public ItemController(_2302b1dotnetContext _db)
        {
            db = _db;
        }
        [Authorize]

        public IActionResult Index()
        {
            var itemdata = db.Items.Include(it => it.Cat);
            return View(itemdata.ToList());
        }
        [Authorize]

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item item, IFormFile file)
        {

            string imageName = DateTime.Now.ToString("yymmddhhmmss");
            imageName += Path.GetFileName(file.FileName);
            var imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/uploads");
            var imageValue = Path.Combine(imagepath, imageName);

            using (var stream = new FileStream(imageValue, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var dbimage = Path.Combine("/uploads", imageName);

            item.Image = dbimage;

            db.Items.Add(item);
            db.SaveChanges();

            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName");

            return RedirectToAction("Index");
        }
        [Authorize]

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = db.Items.Find(id);
            if (item == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName");
                return View(item);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item, IFormFile file, string oldImage)
        {
            var dbimage = "";
            if (file != null && file.Length > 0)
            {
                string imageName = DateTime.Now.ToString("yymmddhhmmss");
                imageName += Path.GetFileName(file.FileName);
                var imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/uploads");
                var imageValue = Path.Combine(imagepath, imageName);
                using (var stream = new FileStream(imageValue, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                dbimage = Path.Combine("/uploads", imageName);
                item.Image = dbimage;
                db.Items.Update(item);
                db.SaveChanges();
            }
            else
            {
                item.Image = oldImage;
                db.Items.Update(item);
                db.SaveChanges();
            }

            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName");
            return RedirectToAction("Index");
        }
        [Authorize]

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = db.Items.Find(id);
            if (item == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View(item);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Item item, IFormFile file, string oldImage)
        {


            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}

