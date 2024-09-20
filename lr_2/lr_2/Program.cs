using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace lr_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json");
            builder.Configuration.AddXmlFile("config.xml");
            builder.Configuration.AddIniFile("config.ini");

            builder.Services.AddSingleton<ICompanyAnalyzer, CompanyAnalyzer>();

            var app = builder.Build();

            var me = app.Configuration.GetSection("Me").Get<Me>();

            app.MapGet("/", async (ICompanyAnalyzer companyAnalyzer, HttpResponse response) =>
            {
                var companies = companyAnalyzer.GetAllCompanies();
                var companyWithMostEmployees = companyAnalyzer.GetCompanyWithMostEmployees(companies);
                foreach (var company in companies) await response.WriteAsync(company.ToString());
                
                await response.WriteAsync($"Company with the most employees: {companyWithMostEmployees.Name} with {companyWithMostEmployees.Employees} employees.");
  
            });

            app.MapGet("/me", async (HttpResponse response) => {
                await response.WriteAsync(me.ToString());
            });

            app.Run();
        }
    }
    public interface ICompanyAnalyzer
    {
        List<Company> GetAllCompanies();
        Company GetCompanyWithMostEmployees(List<Company> companies);
    }

    public class Company
    {
        public string Name { get; set; }
        public int Employees { get; set; }

        public override string ToString()
        {
            return "Name: "+Name+", employees = "+Employees+"\n";
        }
    }

    public class CompanyAnalyzer : ICompanyAnalyzer
    {
        private readonly IConfiguration _configuration;

        public CompanyAnalyzer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Company> GetAllCompanies()
        {
            var companiesSection = _configuration.GetSection("Companies");
            var microsoft = companiesSection.GetSection("Microsoft").Get<Company>();
            var apple = companiesSection.GetSection("Apple").Get<Company>();
            var google = companiesSection.GetSection("Google").Get<Company>();

            var companies = new List<Company> { microsoft, apple, google };
            return companies;
        }
        public Company GetCompanyWithMostEmployees(List<Company> companies)
        {
            return companies.OrderByDescending(c => c.Employees).FirstOrDefault();
        }
    }

    public class Me {
        public string Name { get; set; }
        public int Age { get; set; }
        public string DateOfBirth { get; set; }

        public override string ToString()
        {
            return "\n Name: " + Name + "\n Age = " + Age + "\n Date of birth"+ DateOfBirth;
        }
    }
}