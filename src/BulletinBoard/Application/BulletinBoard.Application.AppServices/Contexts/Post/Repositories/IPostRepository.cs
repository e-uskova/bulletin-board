using BulletinBoard.Contracts;

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
        /// <param name="cancellation">Отмена операции.</param>
        /// <returns>Модель объявления <see cref="PostDto"/></returns>
        Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
