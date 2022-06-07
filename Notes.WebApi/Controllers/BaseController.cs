using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace Notes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        // Оператор объединения с NULL ?? возвращает значение своего операнда слева,
        // если его значение не равно null.
        // В противном случае он вычисляет операнд справа и возвращает его результат.
        // Оператор ?? не выполняет оценку своего операнда справа,
        // если его операнд слева имеет значение, отличное от NULL.
        //сделано это для того, чтобы при обновлении данных, если операнд с права не пустой, то
        //туда записываются значения правого операнда, иначе левого
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();


        // в переменную UserId записывается значение, которое передаётся через
        // тернарный оператор(условие if) которое означаает, что
        // проверяет на наличие пользователя, если такого пользователя нет, то 
        // выполняется 28 строчка кода, иначе 29 (находится пользователь)
        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

    }
}
