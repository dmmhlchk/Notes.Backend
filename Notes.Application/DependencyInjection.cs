using System.Reflection;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Common.Behaviors;

namespace Notes.Application
{
    public static class DependencyInjection
    {
        // метод AddApplication будет добавлять использование логики описанной в теле 
        // метода и регистрировать его
        // иными словами метод AddApplication передаёт необходимую зависимость
        // (медиатр, валидацию) в классы где эта логика упаминается
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddTransient(typeof(IPipelineBehavior<,>), 
                typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
