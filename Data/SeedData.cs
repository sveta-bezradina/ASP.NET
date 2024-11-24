using CompanyApp.Models;
using System.Linq;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (!context.Companies.Any())
        {
            context.Companies.AddRange(
                new Company { Name = "TechCorp", Location = "New York", EmployeeCount = 200 },
                new Company { Name = "Innovatech", Location = "San Francisco", EmployeeCount = 150 },
                new Company { Name = "EcoWorld", Location = "Seattle", EmployeeCount = 100 },
                new Company { Name = "CloudSolutions", Location = "Austin", EmployeeCount = 300 },
                new Company { Name = "GreenTech", Location = "Portland", EmployeeCount = 250 }
            );
            context.SaveChanges();
        }
    }
}
