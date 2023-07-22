using Microsoft.AspNetCore.Builder;

namespace myteam.holiday.WebApi.Middlewares
{
    public static class SwaggerAccessControlMiddlewareExtension
    {
        public static IApplicationBuilder UseSwaggerAccessControl(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SwaggerAccessControlMiddleware>();
        }
    }
}
