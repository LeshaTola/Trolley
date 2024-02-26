using Microsoft.EntityFrameworkCore;
using TrolleyAPI.DataLayer;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCo

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.MapGet("/", (ApplicationContext db) => db.Choices.ToList());

//app.UseWelcomePage();

app.Run();
