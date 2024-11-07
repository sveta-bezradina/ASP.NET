using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace lr_7.Controllers
{
    public class FileController : Controller
    {
        [HttpGet]
        public IActionResult DownloadFile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DownloadFile(string firstName, string lastName, string fileName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(fileName))
            {
                ViewData["Error"] = "Всі поля повинні бути заповнені.";
                return View();
            }

            var content = $"Ім'я: {firstName}\nПрізвище: {lastName}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}.txt");
            System.IO.File.WriteAllText(filePath, content);

            TempData["SuccessMessage"] = "Файл успішно створено!";

            return RedirectToAction("DownloadFile", "File");
        }
    }
}
