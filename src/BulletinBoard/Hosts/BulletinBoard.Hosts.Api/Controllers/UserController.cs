using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.Users;
using Microsoft.AspNetCore.Authorization;
//using BulletinBoard.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями.
    /// </summary>
    [ApiController]
    [Route("/user")]
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

        /// <summary>
        /// Получение пользователей постранично.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <returns>Коллекция пользователей <see cref="UserDto"/></returns>
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUsersAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// Получение пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Модель пользователя <see cref="UserDto"/></returns>
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ActionName(nameof(GetUserAsync))]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetUserAsync(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        /// <summary>
        /// Создание пользователя.
        /// </summary>
        /// <param name="user">Модель для создания пользователя.</param>
        /// <returns>Идентификатор созданной сущности./></returns>
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUserAsync(CreateUserDto user)
        {
            var id = await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetUserAsync), new { id }, id);
        }

        /// <summary>
        /// Редактирование пользователя.
        /// </summary>
        /// <param name="user">Модель для редактирования пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserDto>> EditUserAsync(Guid id, CreateUserDto user)
        {
            await _userService.UpdateAsync(id, user);
            return NoContent();
        }

        /// <summary>
        /// Удаление пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<UserDto>> DeleteUserAsync(Guid id)
        { 
            var result = await _userService.DeleteAsync(id);
            if (result) 
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }

        [AllowAnonymous]
        [HttpPost("public")]
        public JsonResult Public()
        {
            return new JsonResult("Public");
        }

        [Authorize]
        [HttpPost("requiring-auth")]
        //[Authorize(Roles = "Role")]
        [Authorize(Policy = "CustomPolicy")]
        public JsonResult requiringAuth()
        {
            return new JsonResult("Success!");
        }

        [HttpPost("get-user-info")]
        public UserDto GetUserInfo()
        {
            return new UserDto() 
            { 
                Email = HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.ToString(),
            };
        }
    }
}
