using BulletinBoard.Contracts.Attachment;

namespace BulletinBoard.Application.AppServices.Contexts.Attachment.Services
{
    /// <summary>
    /// Версив работы с вложениями.
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// Загрузка файла.
        /// </summary>
        /// <param name="attachment">Файл.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор файла.</returns>
        Task<Guid> UploadAsync(AttachmentDto attachment, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Файл.</returns>
        Task<AttachmentDto> DownloadAsync(Guid id, CancellationToken cancellationToken);
    }
}
