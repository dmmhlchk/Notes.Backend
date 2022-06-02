using System;
using System.Threading;
using System.Threading.Tasks;
using Notes.Application.Interfaces;
using Notes.Application.Common.Exceptions;
using Notes.Domain;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler 
        : IRequestHandler<UpdateNoteCommand>
    {
        // переменная _context  создана для общения с базой данных, в данном классе
        // описывается обновление записи
        private readonly INotesDbContext _context;

        // конструктор, который внедряет зависимость в переменную  _context 
        public UpdateNoteCommandHandler(INotesDbContext context)
            => _context = context;

        // метод Handle реализует интерфейс IRequestHandler<UpdateNoteCommand>
        // с помощью которого мы инкапсулируем нужные нам значения в классе UpdateNoteCommand
        public async Task<Unit> Handle(UpdateNoteCommand request,
            CancellationToken token)
        {
           // ищется необходимая запись (которая передаётся с помощью request.id)
           // конечно если данная запись существует
            var entity =
                await _context.Notes.FirstOrDefaultAsync(note =>
                note.Id == request.Id, token);

            // если записи нету, то выдаётся ошибка, о том, что такой записи нету
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            // логика изменения данных в записе 
            entity.Details = request.Details;
            entity.Title = request.Title;
            entity.EditDate = DateTime.Now;

            // сохранение изменеий в базу данных
            await _context.SaveChangesAsync(token);
            
            // Unit возвращает пустоту(если не ошибаюсь)
            return Unit.Value;
        }

    }
}
