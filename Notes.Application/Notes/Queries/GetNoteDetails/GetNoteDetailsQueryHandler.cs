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
        private readonly INotesDbContext _context;
        private readonly IMapper _mapper;

        public GetNoteDetailsQueryHandler(INotesDbContext context, IMapper mapper)
            => (_context, _mapper) = (context, mapper);

        public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request ,
            CancellationToken token)
        {
            var entity = await _context.Notes
                .FirstOrDefaultAsync(note => note.Id == request.Id, token);

            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            return _mapper.Map<NoteDetailsVm>(entity);
        }
    }
}
