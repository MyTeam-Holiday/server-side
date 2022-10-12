using Microsoft.EntityFrameworkCore;
using myteam.holiday.Domain.Models;
using myteam.holiday.Domain.Services;
using myteam.holiday.EntityFramework.Data;
using myteam.holiday.EntityFramework.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", false) // Common settings
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true) // Development, Stagging or Production settings
    .AddJsonFile("appsettings.Local.json", true, true); // Local settings

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<ModelValidationService>();
builder.Services.AddSingleton<AppDbContextFactory>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITeamRepository, TeamRepository>();
builder.Services.AddTransient<IHolidayRepository, HolidayRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

