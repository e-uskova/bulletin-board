using BulletinBoard.Application.AppServices.Contexts.Post.Services;
using BulletinBoard.Contracts.Post;
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
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель объявления <see cref="PostDto"/></returns>
        [ProducesResponseType(typeof(PostDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PostDto>> GetPostAsync(Guid id)
        {
            var post = await _postService.GetByIdAsync(id);
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
        public async Task<ActionResult<PostDto>> GetPostsAsync()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        /// <summary>
        /// Создание объявления.
        /// </summary>
        /// <param name="dto">Модель для создания объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности./></returns>
        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePostAsync(CreatePostDto post)
        {
            var id = await _postService.AddAsync(post);
            return CreatedAtAction(nameof(GetPostAsync), new { id }, id);
        }

        /// <summary>
        /// Редактирование объявления.
        /// </summary>
        /// <param name="dto">Модель для редактирования объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PostDto>> EditPostAsync(Guid id, CreatePostDto post)
        {
            await _postService.UpdateAsync(id, post);
            return NoContent();
        }

        /// <summary>
        /// Удаление объявления по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PostDto>> DeletePostAsync(Guid id)
        {
            var result = await _postService.DeleteAsync(id);
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