using EducationSearchV3.Data;
using EducationSearchV3.Repositories;
using EducationSearchV3.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddEntities();

builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<EducationSearchV3.Repositories.ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IHighSchoolRepository, HighSchoolRepository>();
builder.Services.AddScoped<IEducationProgramRepository, EducationProgramRepository>();

builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IHighSchoolService, HighSchoolService>();
builder.Services.AddScoped<EducationSearchV3.Services.ICountryService, CountryService>();


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
