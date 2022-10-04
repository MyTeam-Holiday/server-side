using Microsoft.EntityFrameworkCore;
using myteam.holiday.Domain.Models;
using myteam.holiday.Domain.Services;
using myteam.holiday.EntityFramework.Data;
using myteam.holiday.EntityFramework.Services;
using myteam.holiday.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<UserAppDbService>();

builder.Services.AddTransient<IGenericAppDbService<Celebration>, GenericAppDbService<Celebration>>();
builder.Services.AddTransient<IGenericAppDbService<Medical>, GenericAppDbService<Medical>>();
builder.Services.AddTransient<IGenericAppDbService<Team>, GenericAppDbService<Team>>();
builder.Services.AddTransient<IGenericAppDbService<UserRole>, GenericAppDbService<UserRole>>();
builder.Services.AddTransient<IGenericAppDbService<Vacation>, GenericAppDbService<Vacation>>();
builder.Services.AddTransient<IGenericAppDbService<WeatherForecast>, GenericAppDbService<WeatherForecast>>();
builder.Services.AddTransient<IGenericAppDbService<Weekend>, GenericAppDbService<Weekend>>();

builder.Services.AddSingleton<ModelValidationService>();



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

