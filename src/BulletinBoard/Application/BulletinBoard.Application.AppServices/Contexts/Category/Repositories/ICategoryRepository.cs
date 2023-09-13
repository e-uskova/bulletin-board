using BulletinBoard.Contracts.Categories;

namespace BulletinBoard.Application.AppServices.Contexts.Category.Repositories
{
    /// <summary>
    /// Репозиторий для работы с категориями.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Получение категории по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель категории <see cref="CategoryDto"/></returns>
        Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создание категории по модели.
        /// </summary>
        /// <param name="model">Модель категории.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданой сущности.</returns>
        Task<Guid> CreateAsync(Domain.Categories.Category model, CancellationToken cancellationToken);
    }
}
