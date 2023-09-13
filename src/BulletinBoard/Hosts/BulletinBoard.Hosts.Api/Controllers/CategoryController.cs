using BulletinBoard.Application.AppServices.Contexts.Category.Services;
using BulletinBoard.Contracts.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с категориями.
    /// </summary>
    [ApiController]
    [Route("/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Инициализация экземпляра <see cref="PostController"/>
        /// </summary>
        /// <param name="categoryService">Сервис работы с категориями.</param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Получение категории по идентификатору.
        /// </summary>
        /// <remarks>
        /// Пример:
        /// curl -XGET http://host:port/post/get-by-id
        /// </remarks>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель категории <see cref="CategoryDto"/></returns>
        [HttpGet("get-by-id")]
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Получение категорий постранично.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <returns>Коллекция категорий <see cref="CategoryDto"/></returns>
        [HttpGet("get-all-paged")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            return Ok();
        }

        /// <summary>
        /// Создание категории.
        /// </summary>
        /// <param name="dto">Модель для создания категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности./></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken)
        {
            var modelId = await _categoryService.CreateAsync(dto, cancellationToken);
            return Created(nameof(CreateAsync), modelId);
        }

        /// <summary>
        /// Редактирование категории.
        /// </summary>
        /// <param name="dto">Модель для редактирования категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpPut]
        public async Task<IActionResult> UpdateByIdAsync(CategoryDto dto, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Удаление категории по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
