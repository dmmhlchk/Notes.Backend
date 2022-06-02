using System;
using FluentValidation;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNOteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNOteCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(deleteNoteCommand => deleteNoteCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
