using AutoMapper;
using System.Threading.Tasks;
using Notes.WebApi.Models;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Notes.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : BaseController
    {
        private readonly IMapper _mapper;

        public NoteController(IMapper maper) => _mapper = maper;

        // контроллер на получение списка всех записок
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery()
            {
                UserId = UserId
            };

            //отправили запрос
            var vm = await Mediator.Send(query);
            // получили ответ
            return Ok(vm);
        }

        // контроллер на получение конкретной записки
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                UserId = UserId,
                Id = id
            };

            // отправили запрос 
            var vm = await Mediator.Send(query);
            // получили ответ
            return Ok(vm);
        }

        // контроллер для создание новой заметки
        // аттрибут [FromBody] указывает на то что парамметры метода
        // контроллера должнен быть извлечён из данных тела http запроса
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserId;

            // отправили запрос
            var noteId = await Mediator.Send(command);
            // получили ответ
            return Ok(noteId);
        }

        // контроллер на обновление конкретной заметки
        // аттрибут [FromBody] указывает на то что парамметры метода
        // контроллера должнен быть извлечён из данных тела http запроса
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;

            // отправили запрос
            await Mediator.Send(command);
            // получили ответ(в данном случае, мы ничего не получили)
            return NoContent();
        }

        // контроллер на удаление конкретной заметки
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand
            {
                Id = id,
                UserId = UserId
            };

            // отправили запрос
            await Mediator.Send(command);
            // получили ответ(в данном случае, мы ничего не получили)
            return NoContent();
        }

    }
}
