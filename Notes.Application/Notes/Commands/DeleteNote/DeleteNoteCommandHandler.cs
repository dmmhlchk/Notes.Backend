using System.Threading;
using System.Threading.Tasks;
using Notes.Application.Interfaces;
using Notes.Application.Common.Exceptions;
using Notes.Domain;
using MediatR;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler 
        : IRequestHandler<DeleteNoteCommand>
    {
        // переменная _context  создана для общения с базой данных, в данном классе
        // описывается удаление записи
        private readonly INotesDbContext _context;

        // конструктор, который внедряет зависимость в переменную  _context
        public DeleteNoteCommandHandler(INotesDbContext context)
            => _context = context;

        // метод Handle реализует интерфейс IRequestHandler<DeleteNoteCommand>
        // с помощью которого мы инкапсулируем нужные нам значения в классе DeleteNoteCommand
        public async Task<Unit> Handle(DeleteNoteCommand request,
            CancellationToken token)
        {
            // ищется необходимая запись (которая передаётся с помощью request.id)
            // конечно если данная запись существует
            var entity = await _context.Notes
                .FindAsync(new object[] { request.Id }, token);

            // если записи нету, то выдаётся ошибка, о том, что такой записи нету
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            // логика удаление записи
            _context.Notes.Remove(entity);
            await _context.SaveChangesAsync(token);

            // Unit возвращает пустоту(если не ошибаюсь)
            return Unit.Value;
        }
    }
}
