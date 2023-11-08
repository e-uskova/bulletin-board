using BulletinBoard.Contracts.Categories;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Category.Services
{
    /// <summary>
    /// Сервис работы с категориями.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Получение всех элементов. 
        /// </summary>
        /// <returns>Коллекция элементов типа <see cref="CategoryDto"/></returns>
        Task<IEnumerable<CategoryDto>?> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получение элемента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента.</param>
        /// <returns>Элемент типа <see cref="CategoryDto"/></returns>
        Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение категории вместе со всеми дочерними.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<CategoryDto>?> GetWithChildrenByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateCategoryDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Изменение элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid id, EditCategoryDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
