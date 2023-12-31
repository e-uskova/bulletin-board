﻿using BulletinBoard.Contracts.Attachment;

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
        Task<Guid> UploadAsync(AttachmentDto attachment, Guid postId, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Файл.</returns>
        Task<AttachmentDto?> DownloadAsync(Guid id, CancellationToken cancellationToken);
    }
}
