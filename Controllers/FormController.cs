using Microsoft.AspNetCore.Mvc;
using Lr_5_mvc_.Models;


namespace Lr_5_mvc_.Controllers
{
    public class FormController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new FormModel());
        }

        [HttpGet("test/error")]
        public IActionResult GenerateError()
        {
            throw new Exception("This is a test error for logging.");
        }

        [HttpPost]
        public IActionResult Submit(FormModel model)
        {
            if (!string.IsNullOrEmpty(model.Value))
            {
                Response.Cookies.Append("Value", model.Value, new CookieOptions
                {
                    Expires = model.ExpiryDate.ToUniversalTime(),
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

                return RedirectToAction("CheckCookies");
            }
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult CheckCookies()
        {
            if (Request.Cookies.TryGetValue("Value", out var value))
            {
                ViewBag.Message = $"Значення в Cookies: {value}";
            }
            else
            {
                ViewBag.Message = "Cookies не знайдено або вони протерміновані.";
            }

            return View();
        }
    }
}
