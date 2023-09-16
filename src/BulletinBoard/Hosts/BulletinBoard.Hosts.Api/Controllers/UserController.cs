using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.Users;
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

        [HttpGet]
        public async Task<ActionResult<Domain.Users.User>> GetUsersAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Domain.Users.User>> GetUserAsync(Guid id)
        {
            var users = await _userService.GetByIdAsync(id);
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<Domain.Users.User>> CreateUserAsync(Domain.Users.User user)
        {
            await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetUserAsync), new { id = user.Id }, user.Id);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Domain.Users.User>> EditUserAsync(Guid id, Domain.Users.User user)
        {
            var existedUser = await _userService.GetByIdAsync(id);
            if (existedUser == null)
            {
                return NotFound();
            }

            await _userService.UpdateAsync(user);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Domain.Users.User>> DeleteUserAsync(Guid id)
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
