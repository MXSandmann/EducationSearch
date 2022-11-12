using EducationSearchV3.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"-> {conString}");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(conString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Console.WriteLine("-> trying to prepare db");
PrepDB.PrepPopulation(app);

app.Run();
