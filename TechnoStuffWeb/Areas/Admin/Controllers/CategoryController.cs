using Microsoft.AspNetCore.Mvc;
using TechnoStuff.DataAccess.Repository.IRepository;
using TechnoStuff.Models;

namespace TechnoStuffWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IDBRepository _db;

        public CategoryController(IDBRepository db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var categories = _db.Category.GetAll().OrderBy(c => c.DisplayOrder);

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
                _db.Category.Add(category);
                _db.Save();
                TempData["success"] = "Category has been created successfully!";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            var category = _db.Category.GetFirstOrDefault(c => c.Id == id);

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
                _db.Category.Update(category);
                _db.Save();
                TempData["success"] = "Category has been updated successfully!";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Destroy(int? id)
        {
            var category = _db.Category.GetFirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _db.Category.Remove(category);
            _db.Save();
            TempData["success"] = "Category has been deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
