using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase 
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Инициализация экземпляра <see cref="PostController"/>
        /// </summary>
        /// <param name="postService">Сервис работы с пользователями.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("public")]
        public JsonResult Public()
        {
            return new JsonResult("Success!");
        }

        [Authorize]
        //[Authorize(Roles = "Role")]
        //[Authorize(Policy = "Policy")]
        [HttpPost("RequiringAuthorization")]
        public JsonResult RequiringAuthorization() 
        {
            return new JsonResult("Success!");
        }

        [HttpPost("GetUserInfo")]
        public UserDto GetUserInfo()
        {
            return new UserDto
            {
                Scheme = HttpContext.User.Identity.AuthenticationType,
                IsAuthenticated = HttpContext.User.Identity.IsAuthenticated,
                Claims = HttpContext.User.Claims.Select(claim => (object)new { claim.Type, claim.Value }).ToList(),
            };
        }



        /// <summary>
        /// Получение пользователя по идентификатору.
        /// </summary>
        /// <remarks>
        /// Пример:
        /// curl -XGET http://host:port/post/get-by-id
        /// </remarks>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель пользователя <see cref="UserDto"/></returns>
        [HttpGet("get-by-id")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _userService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Получение пользователей постранично.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <returns>Коллекция пользователей <see cref="UserDto"/></returns>
        [HttpGet("get-all-paged")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            return Ok();
        }

        /// <summary>
        /// Создание пользователя.
        /// </summary>
        /// <param name="dto">Модель для создания пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности./></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken)
        {
            var modelId = await _userService.CreateAsync(dto, cancellationToken);
            return Created(nameof(CreateAsync), modelId);
        }

        /// <summary>
        /// Редактирование пользователя.
        /// </summary>
        /// <param name="dto">Модель для редактирования пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpPut]
        public async Task<IActionResult> UpdateByIdAsync(UserDto dto, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
