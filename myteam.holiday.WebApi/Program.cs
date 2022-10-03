using Microsoft.EntityFrameworkCore;
using myteam.holiday.WebApi.Services;
using myteam.holiday.WebServer.Data;
using myteam.holiday.WebServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 10, 1))));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<AppDbWeatherForecastService>();
builder.Services.AddTransient<AppDbUserService>();
builder.Services.AddTransient<AppDbGroupService>();

builder.Services.AddTransient<DbValidationService>();

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
