using BulletinBoard.Contracts;

namespace BulletinBoard.Application.AppServices.Contexts.Post.Services
{
    /// <summary>
    /// Сервис работы с объявлениями.
    /// </summary>
    public interface IPostService
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
