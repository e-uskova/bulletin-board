using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Contracts.Attachment;

namespace BulletinBoard.Application.AppServices.Contexts.Attachment.Services
{
    /// <inheritdoc/>
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="AttachmentService"/>
        /// </summary>
        /// <param name="attachmentRepository">Репозиторий для работы с вложениями.</param>
        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        /// <inheritdoc/>
        public Task<AttachmentInfoDto?> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _attachmentRepository.GetInfoByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<AttachmentDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _attachmentRepository.GetAllAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<AttachmentInfoDto>> GetAllInfoAsync(CancellationToken cancellationToken)
        {
            return _attachmentRepository.GetAllInfoAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _attachmentRepository.DeleteAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> UploadAsync(AttachmentDto attachment, CancellationToken cancellationToken)
        {
            var entity = new Domain.Attachment
            {
                Name = attachment.Name,
                Content = attachment.Content,
                ContentType = attachment.ContentType,
                Created = DateTime.UtcNow,
                Length = attachment.Content.Length,
            };

            return _attachmentRepository.UploadAsync(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<AttachmentDto?> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            return _attachmentRepository.DownloadAsync(id, cancellationToken);
        }
    }
}
