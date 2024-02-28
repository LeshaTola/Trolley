using Microsoft.EntityFrameworkCore;
using TrolleyAPI.DataLayer;
using TrolleyAPI.DataLayer.DTO;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCo

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/Choices", (ApplicationContext db) => db.Choices.ToList());
app.MapGet("/Choices/{level}", (ApplicationContext db, int level) => db.Choices.Where(C => C.Level.Equals(level)).ToList());
app.MapPost("/Choices", (ApplicationContext db, Choice choice) =>
{
	db.Choices.Add(choice);
	db.SaveChanges();
	return choice;
});

app.Run();
