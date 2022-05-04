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
        private readonly INotesDbContext _context;

        public DeleteNoteCommandHandler(INotesDbContext context)
            => _context = context;

        public async Task<Unit> Handle(DeleteNoteCommand request,
            CancellationToken token)
        {
            var entity = await _context.Notes
                .FindAsync(new object[] { request.Id }, token);

            if(entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            _context.Notes.Remove(entity);
            await _context.SaveChangesAsync(token);

            return Unit.Value;
        }
    }
}
