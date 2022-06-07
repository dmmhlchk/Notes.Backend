using AutoMapper;
using System;
using System.Threading.Tasks;
using Notes.WebApi.Models;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Notes.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NoteController : BaseController
    {
        private readonly IMapper _mapper;

        public NoteController(IMapper maper) => _mapper = maper;

        /// <summary>
        /// Gets all notes in list 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /note
        /// </remarks>
        /// <returns>Returns NoteListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Gets note by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /note/2131447E-DF25-4987-AF12-EAC793D4DA40
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Returns NoteDetaisVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <summary>
        /// Creates the note
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /note 
        /// {
        ///     title: "note title"
        ///     detail: "note detail"
        /// }
        /// </remarks>
        /// <param name="createNoteDto">CreateNoteDto object</param>
        /// <returns>Returns id(guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If user is unthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <summary>
        /// Updetes the note
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// PUT /note
        /// {
        ///     title: "update note title"
        ///     detail: "update note detail"
        /// }
        /// </remarks>
        /// <param name="updateNoteDto"></param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If user is unathorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <summary>
        /// Deletes the note by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /note/BA4537F2-CA98-4451-B904-CAA69E88ABDC
        /// </remarks>
        /// <param name="id">ID of the note (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
