using BulletinBoard.Application.AppServices.Contexts.Post.Services;
using BulletinBoard.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        /// <summary>
        /// Инициализация экземпляра <see cref="PostController"/>
        /// </summary>
        /// <param name="postService">Сервис работы с объявлениями.</param>
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Получение объявления по идентификатору.
        /// </summary>
        /// <remarks>
        /// Пример:
        /// curl -XGET http://host:port/post/get-by-id
        /// </remarks>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellation">Отмена операции.</param>
        /// <returns>Модель объявления <see cref="PostDto"/></returns>
        [HttpGet("get-by-id")]
        [ProducesResponseType(typeof(PostDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _postService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Получение объявлений постранично.
        /// </summary>
        /// <param name="cancellation">Отмена операции.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <returns>Коллекция объявлений <see cref="PostDto"/></returns>
        [HttpGet("get-all-paged")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            return Ok();
        }

        /// <summary>
        /// Создание объявления.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellation">Отмена операции.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(PostDto dto, CancellationToken cancellationToken)
        {
            return Created(string.Empty, null);
        }

        /// <summary>
        /// Редактирование объявления.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellation">Отмена операции.</param>
        [HttpPut]
        public async Task<IActionResult> UpdateByIdAsync(PostDto dto, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Удаление объявления по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellation">Отмена операции.</param>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}