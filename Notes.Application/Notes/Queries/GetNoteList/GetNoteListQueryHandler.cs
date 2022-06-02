using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Notes.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;


namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryHandler
        : IRequestHandler<GetNoteListQuery, NoteListVm>
    {
        // переменная _context  создана для общения с базой данных, в данном классе
        // описывается чтение всех имеющихся заметок
        private readonly INotesDbContext _context;

        // переменная _mapper создана для того чтобы маппить объекты,
        // которые понимает пользователь в объекты которые понимает программа
        private readonly IMapper _mapper;

        // конструктор, который внедряет зависимость в переменную  _context и _mapper 
        public GetNoteListQueryHandler(INotesDbContext context, IMapper mapper)
            => (_context, _mapper) = (context, mapper);

        // метод Handle реализует интерфейс IRequestHandler<GetNoteListQuery, NoteListVm>
        // с помощью которого мы инкапсулируем нужные нам значения в классе GetNoteListQuery
        public async Task<NoteListVm> Handle(GetNoteListQuery request,
            CancellationToken token)
        {
            // логика получение всех заметок у пользователя
            var notesQuery = await _context.Notes
                .Where(note => note.UserId == request.UserId)
                .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(token);

            return new NoteListVm { Notes = notesQuery };

        }
    }
}
