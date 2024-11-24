using CompanyApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class CompanyController : Controller
{
    private readonly AppDbContext _context;

    public CompanyController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var companies = _context.Companies.ToList();
        return View(companies);
    }
}
