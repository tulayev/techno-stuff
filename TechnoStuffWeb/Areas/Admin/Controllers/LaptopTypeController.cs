using Microsoft.AspNetCore.Mvc;
using TechnoStuff.DataAccess.Repository.IRepository;
using TechnoStuff.Models;

namespace TechnoStuffWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LaptopTypeController : Controller
    {
        private readonly IDBRepository _db;

        public LaptopTypeController(IDBRepository db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var laptopTypes = _db.LaptopType.GetAll();

            return View(laptopTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LaptopType laptopType)
        {
            if (ModelState.IsValid)
            {
                _db.LaptopType.Add(laptopType);
                _db.Save();
                TempData["success"] = "Laptop Type has been created successfully!";
                return RedirectToAction("Index");
            }

            return View(laptopType);
        }

        public IActionResult Edit(int? id)
        {
            var laptopType = _db.LaptopType.GetFirstOrDefault(lt => lt.Id == id);

            if (laptopType == null)
            {
                return NotFound();
            }

            return View(laptopType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LaptopType laptopType)
        {
            if (ModelState.IsValid)
            {
                _db.LaptopType.Update(laptopType);
                _db.Save();
                TempData["success"] = "Laptop Type has been edited successfully!";
                return RedirectToAction("Index");
            }

            return View(laptopType);
        }

        public IActionResult Destroy(int? id)
        {
            var laptopType = _db.LaptopType.GetFirstOrDefault(lt => lt.Id == id);

            if (laptopType == null)
            {
                return NotFound();
            }

            _db.LaptopType.Remove(laptopType);
            _db.Save();
            TempData["success"] = "Laptop Type has been deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
