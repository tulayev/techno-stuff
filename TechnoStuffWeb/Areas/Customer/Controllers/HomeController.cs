using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechnoStuff.DataAccess.Repository.IRepository;
using TechnoStuff.Models;

namespace TechnoStuffWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IDBRepository _db;

        public HomeController(IDBRepository db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var products = _db.Product.GetAll(includes: "Category,LaptopType");

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var cart = new Cart
            {
                Count = 1,
                Product = _db.Product.GetFirstOrDefault(p => p.Id == id, includes: "Category,LaptopType")
            };

            return View(cart);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}