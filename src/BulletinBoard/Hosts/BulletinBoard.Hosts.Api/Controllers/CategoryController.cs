using BulletinBoard.Application.AppServices.Contexts.Category.Services;
using BulletinBoard.Contracts.Categories;
using BulletinBoard.Contracts.Users;
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
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return BadRequest();
            }
            return Ok(category);
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
        /// <param name="dto">Модель для создания категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности./></returns>
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateUserAsync(CreateCategoryDto category)
        {
            var id = await _categoryService.AddAsync(category);
            return CreatedAtAction(nameof(GetCategoryAsync), new { id }, id);
        }

        /// <summary>
        /// Редактирование категории.
        /// </summary>
        /// <param name="dto">Модель для редактирования категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> EditUserAsync(Guid id, CreateCategoryDto category)
        {
            await _categoryService.UpdateAsync(id, category);
            return NoContent();
        }

        /// <summary>
        /// Удаление категории по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> DeleteUserAsync(Guid id)
        {
            var result = await _categoryService.DeleteAsync(id);
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
