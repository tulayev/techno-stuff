using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechnoStuff.DataAccess.Repository.IRepository;
using TechnoStuff.Models;
using TechnoStuff.Models.ViewModels;

namespace TechnoStuffWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IDBRepository _db;
        private readonly IWebHostEnvironment _env;

        public ProductController(IDBRepository db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var productVM = new ProductVM
            {
                Product = new Product(),
                CategoryList = _db.Category.GetAll().Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
                LaptopTypeList = _db.LaptopType.GetAll().Select(lt => new SelectListItem
                {
                    Value = lt.Id.ToString(),
                    Text = lt.Name
                })
            };

            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //edit
                productVM.Product = _db.Product.GetFirstOrDefault(p => p.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _env.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string uploadsFolder = Path.Combine(wwwRootPath, "images\\products");
                    string extension = Path.GetExtension(file.FileName);

                    if (productVM.Product.Image != null)
                    {
                        string oldImagePath = Path.Combine(wwwRootPath, productVM.Product.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fs = new FileStream(Path.Combine(uploadsFolder, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fs);
                    }

                    productVM.Product.Image = "\\images\\products\\" + fileName + extension;
                }

                if (productVM.Product.Id == 0)
                {
                    _db.Product.Add(productVM.Product);
                }
                else
                {
                    _db.Product.Update(productVM.Product);
                }

                _db.Save();
                TempData["success"] = "Product created successfully!";
                return RedirectToAction("Index");
            }

            return View(productVM);
        }

        #region API
        [HttpGet]
        public IActionResult GetAll() => Json(new { data = _db.Product.GetAll(includes: "Category") });
        
        [HttpDelete]
        public IActionResult Destroy(int? id)
        {
            var product = _db.Product.GetFirstOrDefault(p => p.Id == id);
            
            if (product == null)
            {
                return Json(new { success = false, message = "Delete error" });
            }

            if (product.Image != null)
            {
                string oldImagePath = Path.Combine(_env.WebRootPath, product.Image.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _db.Product.Remove(product);
            _db.Save();
            TempData["success"] = "Product deleted successfully";
            return Json(new { success = true, message = "Removed successfully" });
        }
        #endregion
    }
}
