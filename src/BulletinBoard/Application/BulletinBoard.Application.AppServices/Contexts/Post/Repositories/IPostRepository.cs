using BulletinBoard.Contracts.Post;

namespace BulletinBoard.Application.AppServices.Contexts.Post.Repositories
{
    /// <summary>
    /// Репозиторий для работы с объявлениями.
    /// </summary>
    public interface IPostRepository
    {
        /// <summary>
        /// Получение объявления по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель объявления <see cref="PostDto"/></returns>
        Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создание объявления по модели.
        /// </summary>
        /// <param name="model">Модель объявления.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданой сущности.</returns>
        Task<Guid> CreateAsync(Domain.Posts.Post model, CancellationToken cancellationToken);
    }
}
