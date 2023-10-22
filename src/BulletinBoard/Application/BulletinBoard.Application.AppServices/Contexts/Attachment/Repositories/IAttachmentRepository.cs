using BulletinBoard.Contracts.Attachment;

namespace BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories
{
    /// <summary>
    /// Репозиторий для работы со вложениями.
    /// </summary>
    public interface IAttachmentRepository
    {
        /// <summary>
        /// Получение информации о вложении без контента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор вложения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Информация о файле.</returns>
        Task<AttachmentInfoDto?> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение информации о всех вложениях.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Массив информации о файлах.</returns>
        Task<IEnumerable<AttachmentInfoDto>> GetAllInfoAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Удаление вложения по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор вложения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

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
        Task<AttachmentDto?> DownloadAsync(Guid id, CancellationToken cancellationToken);
    }
}
