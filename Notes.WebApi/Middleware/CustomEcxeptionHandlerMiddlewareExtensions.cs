using Microsoft.AspNetCore.Builder;

namespace Notes.WebApi.Middleware
{
    public static class CustomEcxeptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this
            IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomEcxeptionHandlerMiddleware>();
        }
    }
}
