using Microsoft.AspNetCore.Mvc;
using lr_8.Models;

namespace lr_8.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var products = new List<Product> {
                new Product { Id = 1, Name = "Product 1", Price = 15.5m, CreateDate = DateTime.Now.AddHours(-8) },
                new Product { Id = 2, Name = "Product 1", Price = 8.99m, CreateDate= DateTime.Now.AddHours(-15)},
                new Product { Id = 3, Name = "Product 3", Price = 24.5m, CreateDate= DateTime.Now.AddHours(-10)},
                new Product { Id = 4, Name = "Product 4", Price = 1.99m, CreateDate= DateTime.Now.AddHours(-2)},
            };
            return View(products);
        }
    }
}
