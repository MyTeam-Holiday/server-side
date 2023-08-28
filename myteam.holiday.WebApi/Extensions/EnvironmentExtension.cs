namespace myteam.holiday.WebApi.Extensions
{
	public static class EnvironmentExtension
	{
		private static readonly string EnvName = "dev";

		public static bool IsMyDevelopment(this IWebHostEnvironment env)
		{
			if (env.EnvironmentName == EnvName) return true;
			return false;
		}
	}
}
