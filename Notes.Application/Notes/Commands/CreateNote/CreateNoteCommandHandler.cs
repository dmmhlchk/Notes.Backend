using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler 
        : IRequestHandler<CreateNoteCommand, Guid>
    {
        // переменная _context  создана для общения с базой данных, в данном классе
        // описывается создание новой записи
        private readonly INotesDbContext _context;

        // конструктор, который внедряет зависимость в переменную  _context 
        public CreateNoteCommandHandler(INotesDbContext context)
            => _context = context;

        // метод Handle реализует интерфейс IRequestHandler<CreateNoteCommand, Guid>
        // с помощью которого мы инкапсулируем нужные нам значения в классе CreateNoteCommand
        public async Task<Guid> Handle(CreateNoteCommand reques,
            CancellationToken token)
        {
            // логика создание новой записи
            var note = new Note
            {
                UserId = reques.UserId,
                Title = reques.Title,
                Details = reques.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };

            // передаём нашу созданную запись в сущность Notes описанную в INotesDbContext
            await _context.Notes.AddAsync(note, token);
            await _context.SaveChangesAsync(token);

            return note.Id;
        }

    }
}
