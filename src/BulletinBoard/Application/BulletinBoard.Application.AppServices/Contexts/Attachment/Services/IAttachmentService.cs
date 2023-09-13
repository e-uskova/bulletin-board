using BulletinBoard.Contracts.Attachment;

namespace BulletinBoard.Application.AppServices.Contexts.Attachment.Services
{
    /// <summary>
    /// Версив работы с вложениями.
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// Получение вложения по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор вложения.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель объявления <see cref="PostDto"/></returns>
        Task<AttachmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создание вложения по модели.
        /// </summary>
        /// <param name="model">Модель вложения.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданой сущности.</returns>
        Task<Guid> CreateAsync(CreateAttachmentDto model, CancellationToken cancellationToken);
    }
}
