using lr_10.Models;
using Microsoft.AspNetCore.Mvc;

namespace lr_10.Controllers
{
    public class ConsultationController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationViewModel model)
        {
            if (!model.IsValidConsultationDate())
            {
                ModelState.AddModelError("ConsultationDate", "Консультація щодо продукту 'Основи' не може проходити по понеділках.");
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Success");
            }

            return View(model);
        }


        public IActionResult Success()
        {
            return View();
        }
    }
}
