using BulletinBoard.Application.AppServices.Contexts.Category.Services;
using BulletinBoard.Contracts.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;

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
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ActionName(nameof(GetCategoryAsync))]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetByIdAsync(id, cancellationToken);
            if (category == null)
            {
                return BadRequest();
            }
            return Ok(category);
        }

        [HttpGet("get-with-children")]
        public async Task<ActionResult<ICollection<CategoryDto>>> GetWithChildrenByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetWithChildrenByIdAsync(id, cancellationToken);
            return Ok(categories);
        }

        /// <summary>
        /// Получение категорий постранично.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <returns>Коллекция категорий <see cref="CategoryDto"/></returns>
        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetCategoriessAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Создание категории.
        /// </summary>
        /// <param name="category">Модель для создания категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности./></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategoryAsync(CreateCategoryDto category, CancellationToken cancellationToken)
        {
            var id = await _categoryService.AddAsync(category, cancellationToken);
            return CreatedAtAction(nameof(GetCategoryAsync), new { id }, id);
        }

        /// <summary>
        /// Редактирование категории.
        /// </summary>
        /// <param name="category">Модель для редактирования категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> EditCategoryAsync(Guid id, EditCategoryDto category, CancellationToken cancellationToken)
        {
            await _categoryService.UpdateAsync(id, category, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удаление категории по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.DeleteAsync(id, cancellationToken);
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
