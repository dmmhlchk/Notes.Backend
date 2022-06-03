using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Persistence;
using Notes.Tests.Common;
using Xunit;
using Shouldly;

namespace Notes.Tests.Notes.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteListQueryHandlerTest
    {
        private readonly NotesDbContext _context;
        private readonly IMapper _mapper;

        public GetNoteListQueryHandlerTest(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetNoteListQueryHandler_Success()
        {
            var handler = new GetNoteListQueryHandler(_context, _mapper);

            var result = await handler.Handle(
                new GetNoteListQuery
                {
                    UserId = NotesContextFactory.UserBId
                }, CancellationToken.None);

            result.ShouldBeOfType<NoteListVm>();
            result.Notes.Count.ShouldBe(2);
        }


    }
}
