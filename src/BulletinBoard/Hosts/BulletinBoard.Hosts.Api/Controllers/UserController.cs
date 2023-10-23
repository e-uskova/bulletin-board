using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.Users;
using Microsoft.AspNetCore.Authorization;
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
        /// Получение всех пользователей.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Коллекция пользователей <see cref="UserDto"/></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// Получение пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Модель пользователя <see cref="UserDto"/></returns>
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize]
        [ActionName(nameof(GetUserAsync))]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetUserAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        /// <summary>
        /// Регистрация.
        /// </summary>
        /// <param name="user">Модель для создания пользователя.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Идентификатор созданной сущности./></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUserAsync(CreateUserDto user, CancellationToken cancellationToken)
        {
            var existedUser = await _userService.GetFirstWhere(u => u.Email == user.Email);
            if (existedUser != null)
            {
                return BadRequest("Пользователь с такой почтой уже зарегистрирован.");
            }

            var id = await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetUserAsync), new { id }, id);
        }

        /// <summary>
        /// Редактирование пользователя.
        /// </summary>
        /// <param name="user">Модель для редактирования пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserDto>> EditUserAsync(Guid id, CreateUserDto user, CancellationToken cancellationToken)
        {
            var idFromClaims = HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "Id")?.Value;
            if (idFromClaims == null)
            {
                return Unauthorized();
            }
            if (Guid.Parse(idFromClaims) != id)
            {
                return BadRequest("Нельзя редактировать чужой профиль.");
            }

            await _userService.UpdateAsync(Guid.Parse(idFromClaims), user);
            return NoContent();
        }

        /// <summary>
        /// Удаление пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<UserDto>> DeleteUserAsync(Guid id, CancellationToken cancellationToken)
        {
            var idFromClaims = HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "Id")?.Value;
            if (idFromClaims == null)
            {
                return Unauthorized();
            }
            if (Guid.Parse(idFromClaims) != id)
            {
                return BadRequest("Нельзя удалять чужой профиль.");
            }

            var result = await _userService.DeleteAsync(Guid.Parse(idFromClaims));
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
        //[Authorize(Roles = "User")]
        //[Authorize(Policy = "CustomPolicy")]
        public JsonResult requiringAuth()
        {
            return new JsonResult("Success!");
        }

        [Authorize]
        [HttpPost("requiring-auth-admin")]
        [Authorize(Roles = "Admin")]
        public JsonResult requiringAuthAdmin()
        {
            return new JsonResult("Success!");
        }

        [Authorize]
        [HttpPost("get-user-info")]
        public async Task<UserDto> GetCurrentUserInfo(CancellationToken cancellationToken)
        {
            var idFromClaims = HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "Id")?.Value;
            if (idFromClaims == null)
            {
                return new UserDto();
            }

            return await _userService.GetByIdAsync(Guid.Parse(idFromClaims));
        }
    }
}
