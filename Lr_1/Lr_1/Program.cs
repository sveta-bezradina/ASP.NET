var builder = WebApplication.CreateBuilder();

var app = builder.Build();


Company c1 = new Company("The Coca-Cola", "The Coca-Cola Company and Coca-Cola HBC have been working, investing and supporting Ukrainian society for 30 years.");

app.MapGet("/", async context => await context.Response.WriteAsync(c1.ToString()));

Random rnd = new Random();
app.MapGet("/numb", () => rnd.Next(1, 101).ToString());
app.Run();

class Company
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Company(string name, string descr) { 
        Name = name;
        Description = descr;
    }
    public override string ToString()
    {
        return "Name: " + Name.ToString() + " Description: " + Description.ToString();
    }
}
