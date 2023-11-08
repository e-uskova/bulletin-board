using BulletinBoard.Application.AppServices.Contexts.Category.Services;
using BulletinBoard.Contracts.Categories;
using Microsoft.AspNetCore.Authorization;
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
        /// Создание категории.
        /// </summary>
        /// <param name="category">Модель для создания категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности./></returns>
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.Created)]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategoryAsync(CreateCategoryDto category, CancellationToken cancellationToken)
        {
            var id = await _categoryService.AddAsync(category, cancellationToken);
            if (id == Guid.Empty)
            {
                return BadRequest("Не найдена родительская категория.");
            }
            return CreatedAtAction(nameof(GetCategoryAsync), new { id }, id);
        }

        /// <summary>
        /// Получение всех категорий.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Коллекция категорий <see cref="CategoryDto"/></returns>
        [ProducesResponseType(typeof(List<CategoryDto>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAllAsync(cancellationToken);
            return Ok(categories);
        }

        /// <summary>
        /// Получение категории по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель категории <see cref="CategoryDto"/></returns>
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ActionName(nameof(GetCategoryAsync))]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetByIdAsync(id, cancellationToken);
            return category == null ? BadRequest() : Ok(category);
        }

        /*/// <summary>
        /// Получение категории со всеми потомками по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Коллекция категорий <see cref="CategoryDto"/></returns>
        [ProducesResponseType(typeof(List<CategoryDto>), (int)HttpStatusCode.OK)]
        [HttpGet("get-with-children")]
        public async Task<ActionResult<CategoryDto>> GetWithChildrenByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetWithChildrenByIdAsync(id, cancellationToken);
            return Ok(categories);
        }*/

        /// <summary>
        /// Редактирование категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="category">Модель для редактирования категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> EditCategoryAsync(Guid id, EditCategoryDto category, CancellationToken cancellationToken)
        {
            var result = await _categoryService.UpdateAsync(id, category, cancellationToken);
            return result ? BadRequest() : Ok();
        }

        /// <summary>
        /// Удаление категории по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.DeleteAsync(id, cancellationToken);
            return result ? BadRequest("Категория не найдена.") : Ok();
        }
    }
}
