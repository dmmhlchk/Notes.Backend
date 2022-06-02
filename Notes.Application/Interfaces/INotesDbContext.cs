using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;

namespace Notes.Application.Interfaces
{
    // INotesDbContext интерфейс который будет реализовывать NotesDbContext
    // находящийся в проекте Notes.Persistence
    // то есть интерфейс это часть приложения, а реализация во вне
    public interface INotesDbContext
    {
        //DbSet описывает сущности с котороми в дальнейшем придётся работать
        //в коде для создания CRUD запросов и их сценариев
        DbSet<Note> Notes { get; set; }

        //SaveChangesAsyn сохраняет изменения контекста в базу данных
        // CancellationToken исполльзуется для аварийного прерывания методов Task
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
