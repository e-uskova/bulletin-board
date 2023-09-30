using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<ActionResult<User>> GetUsersAsync()
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
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User>> GetUserAsync(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        /// <summary>
        /// Создание пользователя.
        /// </summary>
        /// <param name="dto">Модель для создания пользователя.</param>
        /// <returns>Идентификатор созданной сущности./></returns>
        [HttpPost]
        public async Task<ActionResult<User>> CreateUserAsync(User user)
        {
            await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetUserAsync), new { id = user.Id }, user.Id);
        }

        /// <summary>
        /// Редактирование пользователя.
        /// </summary>
        /// <param name="dto">Модель для редактирования пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<User>> EditUserAsync(Guid id, User user)
        {
            var existedUser = await _userService.GetByIdAsync(id);
            if (existedUser == null)
            {
                return NotFound();
            }

            await _userService.UpdateAsync(user);

            return NoContent();
        }

        /// <summary>
        /// Удаление пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<User>> DeleteUserAsync(Guid id)
        {
            var existedUser = await _userService.GetByIdAsync(id);
            if (existedUser == null)
            {
                return NotFound();
            }

            await _userService.DeleteAsync(existedUser);

            return NoContent();
        }
    }
}
