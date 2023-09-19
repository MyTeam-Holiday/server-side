using myteam.holiday.WebApi;

namespace myteam.holiday.init
{
	public partial class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
					webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
					{
						string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
						string basePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings");

						string appSettingsFile = $"appsettings.{env}.json";
						string appSettingsPath = Path.Combine(basePath, appSettingsFile);

						config.AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true);
					});
				});
	}
}