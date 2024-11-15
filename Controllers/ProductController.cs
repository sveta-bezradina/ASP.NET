using lr_9.Models;
using Microsoft.AspNetCore.Mvc;

namespace lr_9.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product { ID = 1, Name = "Product 1", Price = 10.5M },
                new Product { ID = 2, Name = "Product 2", Price = 20.99M },
                new Product { ID = 3, Name = "Product 3", Price = 30.75M }
            };

            return View(products); // Передаємо список продуктів в подання
        }
    }
}
