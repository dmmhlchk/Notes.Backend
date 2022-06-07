using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Notes.Application.Common.Exceptions;

namespace Notes.WebApi.Middleware
{
    public class CustomEcxeptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomEcxeptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        //метод Invoke пытается вызвать RequestDelegate _next
        //затем перехватывает и обрабатывает исключения
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        //данный метод обрабатывает два исключения 
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; //код ответа
            var result = string.Empty;

            switch (exception)
            {
                //исключение валидации
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;

                //исключение не найдена сущность
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;

            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if(result == String.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }

    }
}
