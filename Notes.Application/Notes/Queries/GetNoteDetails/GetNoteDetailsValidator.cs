using System;
using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsValidator : AbstractValidator<GetNoteDetailsQuery>
    {
        public GetNoteDetailsValidator()
        {
            RuleFor(note => note.Id).NotEqual(Guid.Empty);
            RuleFor(note => note.UserId).NotEqual(Guid.Empty);
        }
    }
}