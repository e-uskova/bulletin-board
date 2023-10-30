﻿using BulletinBoard.Application.AppServices.Contexts.Post.Services;
using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с объявлением.
    /// </summary>
    [ApiController]
    [Route("/post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        /// <summary>
        /// Инициализация экземпляра <see cref="PostController"/>
        /// </summary>
        /// <param name="postService">Сервис работы с объявлениями.</param>
        /// <param name="userService">Сервис работы с пользователями.</param>
        public PostController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        /// <summary>
        /// Получение объявления по идентификатору.
        /// </summary>
        /// <remarks>
        /// Пример:
        /// curl -XGET http://host:port/post/get-by-id
        /// </remarks>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель объявления <see cref="PostDto"/></returns>
        [ProducesResponseType(typeof(PostDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ActionName(nameof(GetPostAsync))]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PostDto>> GetPostAsync(Guid id, CancellationToken cancellationToken)
        {
            var post = await _postService.GetByIdAsync(id, cancellationToken);
            if (post == null)
            {
                return BadRequest();
            }
            return Ok(post);
        }

        /// <summary>
        /// Получение объявлений постранично.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <returns>Коллекция объявлений <see cref="PostDto"/></returns>
        [HttpGet]
        public async Task<ActionResult<PostDto>> GetPostsAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            var posts = await _postService.GetAllAsync(cancellationToken, pageSize, pageIndex);
            return Ok(posts);
        }

        /// <summary>
        /// Создание объявления.
        /// </summary>
        /// <param name="dto">Модель для создания объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности./></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePostAsync(CreatePostDto post, CancellationToken cancellationToken)
        {
            var emailFromClaims = HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (emailFromClaims == null)
            {
                return Unauthorized();
            }
            var curUser = await _userService.GetFirstWhere(u => u.Email == emailFromClaims, cancellationToken);

            var id = await _postService.AddAsync(post, curUser, cancellationToken);
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetPostAsync), new { id }, id);
        }

        /// <summary>
        /// Редактирование объявления.
        /// </summary>
        /// <param name="dto">Модель для редактирования объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PostDto>> EditPostAsync(Guid id, EditPostDto post, CancellationToken cancellationToken)
        {
            await _postService.UpdateAsync(id, post, cancellationToken);
            return NoContent();
        }

        [Authorize]
        [HttpPut("close/{id:guid}")]
        public async Task<ActionResult<PostDto>> ClosePostAsync(Guid id, CancellationToken cancellationToken)
        {
            await _postService.CloseAsync(id, cancellationToken);
            return NoContent();
        }

        [Authorize]
        [HttpPut("reopen/{id:guid}")]
        public async Task<ActionResult<PostDto>> ReOpenAsync(Guid id, CancellationToken cancellationToken)
        {
            await _postService.ReOpenAsync(id, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удаление объявления по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PostDto>> DeletePostAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _postService.DeleteAsync(id, cancellationToken);
            if (result)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }
    }
}