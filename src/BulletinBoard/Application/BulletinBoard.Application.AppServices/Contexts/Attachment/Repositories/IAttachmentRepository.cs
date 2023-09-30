using BulletinBoard.Contracts.Attachment;

namespace BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories
{
    /// <summary>
    /// Репозиторий для работы со вложениями.
    /// </summary>
    public interface IAttachmentRepository
    {
        /// <summary>
        /// Получение вложения по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор вложения.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель вложения <see cref="PostDto"/></returns>
        Task<AttachmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создание вложения по модели.
        /// </summary>
        /// <param name="model">Модель вложения.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданой сущности.</returns>
        Task<Guid> CreateAsync(Domain.Attachment model, CancellationToken cancellationToken);
    }
}
