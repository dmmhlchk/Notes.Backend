using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Notes.Domain;
using Notes.Application.Interfaces;
using Notes.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryHandler 
        : IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
    {
        // переменная _context  создана для общения с базой данных, в данном классе
        // описывается чтение конкретной заметки
        private readonly INotesDbContext _context;

        // переменная _mapper создана для того чтобы маппить объекты,
        // которые понимает пользователь в объекты которые понимает программа
        private readonly IMapper _mapper;

        // конструктор, который внедряет зависимость в переменную  _context и _mapper
        public GetNoteDetailsQueryHandler(INotesDbContext context, IMapper mapper)
            => (_context, _mapper) = (context, mapper);

        // метод Handle реализует интерфейс IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
        // с помощью которого мы инкапсулируем нужные нам значения в классе GetNoteDetailsQuery
        public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request ,
            CancellationToken token)
        {
            // в переменную entity передаётся значение найденой записи
            var entity = await _context.Notes
                .FirstOrDefaultAsync(note => note.Id == request.Id, token);

            // если запись или пользователь не найден, то вернётся ошибка  
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            // если всё удовлетворяет условия, то пользователю покажется
            // конкретная запись
            return _mapper.Map<NoteDetailsVm>(entity);
        }
    }
}
