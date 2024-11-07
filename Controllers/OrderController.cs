using lr_6.Models;
using Microsoft.AspNetCore.Mvc;

namespace lr_6.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user) 
        {
            if (user.Age >= 16) {
                return RedirectToAction("SelectProductQuantity", new { userId = user.Id });
            }
            return View("AgeRestriction");
        }

        [HttpGet]
        public IActionResult SelectProductQuantity(int userId) 
        {
            return View(new User { Id = userId});
        }

        [HttpPost]
        public IActionResult SelectProductQuantity(User user) 
        {
            if (user.ProductQuantity > 0)
            {
                return RedirectToAction("OrderProducts", new { quantity = user.ProductQuantity });
            }
            ModelState.AddModelError("ProductQuantity", "Кількість повинна бути цілим невід'ємним числом.");
            return View(user);
        }

        [HttpGet]
        public IActionResult OrderProducts(int quantity)
        {
            var products = new List<Product>();
            for (int i=0; i<quantity; i++)
            {
                products.Add(new Product());
            }
            return View(products);
        }

        [HttpPost]
        public IActionResult OrderProducts(List<Product> products)
        {
            return View("OrderSummary", products);
        }
    }
}
