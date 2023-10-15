using BulletinBoard.Contracts.Attachment;

namespace BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories
{
    /// <summary>
    /// Репозиторий для работы со вложениями.
    /// </summary>
    public interface IAttachmentRepository
    {
        /// <summary>
        /// Загрузка файла.
        /// </summary>
        /// <param name="attachment">Файл.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор файла.</returns>
        Task<Guid> UploadAsync(Domain.Attachment attachment, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Файл.</returns>
        Task<AttachmentDto> DownloadAsync(Guid id, CancellationToken cancellationToken);
    }
}
