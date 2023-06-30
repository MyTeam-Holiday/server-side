using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using myteam.holiday.Domain.Models;
using myteam.holiday.Domain.Services;
using myteam.holiday.EntityFramework.Data;
using myteam.holiday.EntityFramework.Services;
using System.Net.Http.Headers;
using System.Text.Json;

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

builder.Services.AddAuthentication("cookie")
    .AddCookie("cookie")
    .AddGoogle("google", o =>
    {
        o.SignInScheme = "cookie";

        o.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        o.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

        o.Events.OnCreatingTicket = async ctc =>
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, o.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ctc.AccessToken);
            using var response = await ctc.Backchannel.SendAsync(request);
            var userData = await response.Content.ReadFromJsonAsync<JsonElement>();
            ctc.RunClaimActions(userData);
        };
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

