using Microsoft.AspNetCore.Builder;

namespace Notes.WebApi.Middleware
{
    public static class CustomEcxeptionHandlerMiddlewareExtensions
    {
        //данный метод вызывается в классе Startup в методе Configure, куда передаются все
        //обработчики запросов(middleware)
        //о middleware там написано больше
        public static IApplicationBuilder UseCustomExceptionHandler(this
            IApplicationBuilder builder)
        {
            //сама логика обработчика находится в классе CustomExceptionHandlerMiddleware
            return builder.UseMiddleware<CustomEcxeptionHandlerMiddleware>();
        }
    }
}
