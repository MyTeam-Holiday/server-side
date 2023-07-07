using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using myteam.holiday.Domain.Services;
using myteam.holiday.EntityFramework.Data;
using myteam.holiday.EntityFramework.Services;

namespace myteam.holiday.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Метод, вызываемый для конфигурации сервисов приложения
        public void ConfigureServices(IServiceCollection services)
        {
            // Добавление сервисов, необходимых для приложения
            services.AddControllers();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            // Add a Swagger generator and Automatic Request and Response annotations:
            //services.AddSwaggerSwashbuckleConfigured();
            services.AddEndpointsApiExplorer();
            //services.AddSingleton<ModelValidationService>();
            services.AddSingleton<AppDbContextFactory>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<IHolidayRepository, HolidayRepository>();

            // Конфигурация сервисов из appsettings.json
            //var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //var basePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings");

            //var appSettingsFile = $"appsettings.{env}.json";
            //var appSettingsPath = Path.Combine(basePath, appSettingsFile);

            //var configurationBuilder = new ConfigurationBuilder()
            //    .SetBasePath(basePath)
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddJsonFile(appSettingsFile, optional: true, reloadOnChange: true);

            //Configuration = configurationBuilder.Build();
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Добавление Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyTeam.holiday API", Version = "v1" });
            });

            // Другие настройки сервисов
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie("cookie")
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddGoogle(o =>
                {
                    o.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    //o.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                    //o.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                });
            services.AddAuthorization(o =>
            {
                o.AddPolicy("Roles", policy =>
                {
                    policy.RequireRole("Moderator", "Admin")
                    .RequireAuthenticatedUser();
                });
            });
        }

        // Метод, вызываемый для настройки конвейера обработки запросов
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyTeam.holiday API v1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // Другие конечные точки маршрутизации
            });
            
            //app.MapControllers();

            // Место для использования app.Run()
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello, World!");
            });
        }
    }
}