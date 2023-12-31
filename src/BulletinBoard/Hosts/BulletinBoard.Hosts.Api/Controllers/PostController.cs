﻿using BulletinBoard.Application.AppServices.Contexts.Post.Services;
using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Contracts.User;
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
        /// Создание объявления.
        /// </summary>
        /// <param name="post">Модель для создания объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности./></returns>
        [ProducesResponseType(typeof(PostDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePostAsync(CreatePostDto post, CancellationToken cancellationToken)
        {
            var curUser = await GetCurrentUserAsync(cancellationToken);
            if (curUser == null)
            {
                return Unauthorized();
            }

            var id = await _postService.AddAsync(post, curUser, cancellationToken);
            return id == Guid.Empty ? BadRequest() : CreatedAtAction(nameof(GetPostAsync), new { id }, id);
        }

        /// <summary>
        /// Получение объявлений постранично.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <returns>Коллекция объявлений <see cref="PostDto"/></returns>
        [ProducesResponseType(typeof(PostDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<ActionResult<PostDto>> GetPostsAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            var posts = await _postService.GetAllAsync(cancellationToken, pageSize, pageIndex);
            return posts == null ? BadRequest() : Ok(posts);
        }

        /// <summary>
        /// Получение объявления по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель объявления <see cref="PostDto"/></returns>
        [ProducesResponseType(typeof(PostDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ActionName(nameof(GetPostAsync))]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PostDto>> GetPostAsync(Guid id, CancellationToken cancellationToken)
        {
            var post = await _postService.GetByIdAsync(id, cancellationToken);
            return post == null ? BadRequest("Объявление не найдено.") : Ok(post);
        }

        /// <summary>
        /// Редактирование объявления.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post">Модель для редактирования объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [ProducesResponseType(typeof(PostDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PostDto>> EditPostAsync(Guid id, EditPostDto post, CancellationToken cancellationToken)
        {
            var authResult = await AuthorizeUserAsync(id, cancellationToken);
            if (authResult != StatusCodes.Status200OK)
            {
                return new StatusCodeResult(authResult);
            }

            var result = await _postService.UpdateAsync(id, post, cancellationToken);
            return result ? BadRequest() : Ok();
        }

        /// <summary>
        /// Деактивация объявления.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PostDto), (int)HttpStatusCode.OK)]
        [Authorize]
        [HttpPut("{id:guid}/close")]
        public async Task<ActionResult<PostDto>> ClosePostAsync(Guid id, CancellationToken cancellationToken)
        {
            var authResult = await AuthorizeUserAsync(id, cancellationToken);
            if (authResult != StatusCodes.Status200OK)
            {
                return new StatusCodeResult(authResult);
            }

            var result = await _postService.CloseAsync(id, cancellationToken);
            return result ? BadRequest() : Ok();
        }

        /// <summary>
        /// Активация объявления.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PostDto), (int)HttpStatusCode.OK)]
        [Authorize]
        [HttpPut("{id:guid}/reopen")]
        public async Task<ActionResult<PostDto>> ReOpenAsync(Guid id, CancellationToken cancellationToken)
        {
            var authResult = await AuthorizeUserAsync(id, cancellationToken);
            if (authResult != StatusCodes.Status200OK)
            {
                return new StatusCodeResult(authResult);
            }

            var result = await _postService.ReOpenAsync(id, cancellationToken);
            return result ? BadRequest() : Ok();
        }

        /// <summary>
        /// Удаление объявления по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [ProducesResponseType(typeof(PostDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PostDto>> DeletePostAsync(Guid id, CancellationToken cancellationToken)
        {
            var roleFromClaims = HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;
            if (roleFromClaims != "Admin")
            {
                var authResult = await AuthorizeUserAsync(id, cancellationToken);
                if (authResult != StatusCodes.Status200OK)
                {
                    return new StatusCodeResult(authResult);
                }
            }

            var result = await _postService.DeleteAsync(id, cancellationToken);
            return result ? BadRequest() : Ok();
        }

        /// <summary>
        /// Получение текущего пользователя.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель пользователя.</returns>
        private async Task<UserDto?> GetCurrentUserAsync(CancellationToken cancellationToken)
        {
            var idFromClaims = HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "Id")?.Value;
            if (idFromClaims == null)
            {
                return null;
            }

            var curUser = await _userService.GetByIdAsync(Guid.Parse(idFromClaims), cancellationToken);
            return curUser;
        }

        /// <summary> 
        /// Проверка, что текущий пользователь является автором объявления.
        /// </summary>
        /// <param name="postId">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Код статуса.</returns>
        private async Task<int> AuthorizeUserAsync(Guid postId, CancellationToken cancellationToken)
        {
            var curUser = await GetCurrentUserAsync(cancellationToken);
            if (curUser == null)
            {
                return StatusCodes.Status401Unauthorized;
            }

            var postEntity = await _postService.GetByIdAsync(postId, cancellationToken);
            if (postEntity != null && postEntity.AuthorId != curUser.Id)
            {
                return StatusCodes.Status400BadRequest;
            }
            return StatusCodes.Status200OK;
        }
    }
}