using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using myteam.holiday.Domain.Services;
using myteam.holiday.EntityFramework.Data;
using myteam.holiday.EntityFramework.Services;
using myteam.holiday.WebApi.EmailService;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", false) // Common settings
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true) // Development, Stagging or Production settings
    .AddJsonFile("appsettings.Local.json", true, true); // Local settings


builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
//services.AddValidatorsFromAssemblyComtaining<MyValidator>();

// Configure the API versioning properties of the project. 
//builder.Services.AddApiVersioningConfigured();

// Add a Swagger generator and Automatic Request and Response annotations:
//builder.Services.AddSwaggerSwashbuckleConfigured();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<ModelValidationService>();
builder.Services.AddSingleton<AppDbContextFactory>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITeamRepository, TeamRepository>();
builder.Services.AddTransient<IHolidayRepository, HolidayRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie("cookie")
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddGoogle(o =>
    {
        o.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        o.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        o.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];        
    });

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Roles", policy =>
    {
        policy.RequireRole("Moderator", "Admin")
              .RequireAuthenticatedUser();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

