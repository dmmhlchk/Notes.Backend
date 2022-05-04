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
        private readonly INotesDbContext _context;

        public CreateNoteCommandHandler(INotesDbContext context)
            => _context = context;

        public async Task<Guid> Handle(CreateNoteCommand reques,
            CancellationToken token)
        {
            var note = new Note
            {
                UserId = reques.UserId,
                Title = reques.Title,
                Details = reques.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };

            await _context.Notes.AddAsync(note, token);
            await _context.SaveChangesAsync(token);

            return note.Id;
        }

    }
}
