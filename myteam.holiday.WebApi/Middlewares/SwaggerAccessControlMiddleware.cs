namespace myteam.holiday.WebApi.Middlewares
{
    public class SwaggerAccessControlMiddleware
    {
        private readonly RequestDelegate _next;

        public SwaggerAccessControlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger") && !context.User.IsInRole("Developer"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}
