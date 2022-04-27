using Microsoft.AspNetCore.Mvc;
using TechnoStuff.DataAccess.Repository.IRepository;
using TechnoStuff.Models;

namespace TechnoStuffWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.GetAll();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Add(category);
                _db.Save();
                TempData["success"] = "Category has been created successfully!";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _db.GetFirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Update(category);
                _db.Save();
                TempData["success"] = "Category has been updated successfully!";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Destroy(int? id)
        {
            var category = _db.GetFirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _db.Remove(category);
            _db.Save();
            TempData["success"] = "Category has been deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
