using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Domain;
using Notes.Persistence.EntityTypeConfiguration;

namespace Notes.Persistence
{
    public class NotesDbContext : DbContext, INotesDbContext
    {

        //DbSet описывает сущности с которой в дальнейшем придётся работать
        //в коде. К примеру для создания CRUD запросов, или просто обращаться к данной сущности
        public DbSet<Note> Notes { get; set; }

        // подключение к базe данных, сама строка подключения находится на 
        // уровне ниже, в Notes.WebApi в json файле
        public NotesDbContext(DbContextOptions<NotesDbContext> options)
            : base(options) { }

        //метод onModelCreatin нужен для определения конфигураций сущностей, 
        //которые уже описаны в папке EntityTypeConfiguration
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NoteConfiguration());
            base.OnModelCreating(builder);
        }

    }
}

