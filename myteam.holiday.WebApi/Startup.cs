using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using myteam.holiday.WebApi.Extensions;
using myteam.holiday.Domain.Services;
using myteam.holiday.EntityFramework.Data;
using myteam.holiday.EntityFramework.Services;
using myteam.holiday.WebApi.EmailService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using myteam.holiday.Domain.Models;

namespace myteam.holiday.WebApi
{
    public class Startup
    {
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
        {
            string? x = configuration["EmailConfig:EmailAddress"];
		}

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

			// Добавление Swagger
			services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyTeam.holiday API", Version = "v1" });
            });

			//добавление дб контекста для ef identity
			services.AddDbContext<IdentityContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion("8.0.33")));
            
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
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
					/*.AddGoogle(GoogleDefaults.AuthenticationScheme, o =>
                    {
                        o.SignInScheme = IdentityConstants.ExternalScheme;
                        o.ClientId = Configuration["Auth0:ClientId"];
                        o.ClientSecret = Configuration["Auth0:ClientSecret"];
                    })
					.AddOAuth("Vk", options =>
                    {
                        options.ClientId = "51720199";
                        options.ClientSecret = "L4jaPLS00SkVMWqsnLBX";
                        options.CallbackPath = new PathString("/signin-vk");
                        options.AuthorizationEndpoint = "https://oauth.vk.com/authorize";
                        options.TokenEndpoint = "https://oauth.vk.com/access_token";
                        options.UserInformationEndpoint = "https://api.vk.com/method/users.get?v=5.131";
                        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "first_name");
                        options.SaveTokens = true;
                    });*/

			services.AddAuthorization();
        }

        // Метод, вызываемый для настройки конвейера обработки запросов
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {  
            if (env.IsMyDevelopment())
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