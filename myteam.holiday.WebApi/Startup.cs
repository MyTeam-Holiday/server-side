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
using myteam.holiday.WebApi.EmailService;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Identity;
using myteam.holiday.Domain.Models;
using myteam.holiday.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.Google;

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
            services.AddTransient<IEmailSender, EmailSender>();      

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

            //добавление дб контекста для ef identity
            services.AddDbContext<IdentityContext>(options =>
                options.UseMySql(Configuration.GetConnectionString(""), new MySqlServerVersion("8.0.33")));
            
            services.AddIdentity<AppUser, IdentityRole>(o =>
            {
                //настройки валидации юзера и пароля 
                o.User.RequireUniqueEmail = true;
                o.SignIn.RequireConfirmedEmail = true;                

            }).AddDefaultTokenProviders()
              .AddEntityFrameworkStores<IdentityContext>();

            //настройка опций провайдера токенов
            services.Configure<DataProtectionTokenProviderOptions>(o =>
            {
                o.TokenLifespan = TimeSpan.FromHours(1);
            });

            // Другие настройки сервисов
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddGoogle(GoogleDefaults.AuthenticationScheme ,o =>
                    {
                        o.SignInScheme = IdentityConstants.ExternalScheme;
                        o.ClientId = Configuration["Auth0:ClientId"];
                        o.ClientSecret = Configuration["Auth0:ClientSecret"];
                    });

            services.AddAuthorization();
        }

        // Метод, вызываемый для настройки конвейера обработки запросов
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {  
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //мидлвер для проверки роли юзера для доступа к сваггеру.
                //раскоментить, когда будет готова страница для логина  
                //app.UseSwaggerAccessControl();

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
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
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