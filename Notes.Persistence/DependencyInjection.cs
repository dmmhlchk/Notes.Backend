using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Notes.Application.Interfaces;

namespace Notes.Persistence
{
    public static class DependencyInjection
    {
        // метод AddPersistance будет добавлять использование контекста базы данных
        // и регистрировать его
        // иными словами метод AddPersistance передаёт необходимую зависимость
        // (контекст базы данных) в классы где этот контекст упаминается 
        public static IServiceCollection AddPersistence(
            this IServiceCollection services, IConfiguration configuration )
        {
            // строка подключения к самой базе данных
            // переменная DBconnection описана в json файле 
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<NotesDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<INotesDbContext>(provider =>
            provider.GetService<NotesDbContext>());

            return services;
        }

    }

}
