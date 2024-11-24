using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CompanyDb;Trusted_Connection=True;");
        }
    }
}
