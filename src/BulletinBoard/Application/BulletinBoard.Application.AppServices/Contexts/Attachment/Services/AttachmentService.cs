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
        public Task<AttachmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _attachmentRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(CreateAttachmentDto model, CancellationToken cancellationToken)
        {
            var attachment = new Domain.Attachments.Attachment()
            {
                Title = model.Title,
            };
            return _attachmentRepository.CreateAsync(attachment, cancellationToken);
        }
    }
}
