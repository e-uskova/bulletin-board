using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Contracts.Attachment;

namespace BulletinBoard.Application.AppServices.Contexts.Attachment.Services
{
    /// <inheritdoc/>
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        //private readonly IUserRepository _userRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="AttachmentService"/>
        /// </summary>
        /// <param name="attachmentRepository">Репозиторий для работы с вложениями.</param>
        public AttachmentService(IAttachmentRepository attachmentRepository/*, IUserRepository userRepository*/)
        {
            _attachmentRepository = attachmentRepository;
            //_userRepository = userRepository;
        }

        /// <inheritdoc/>
        public Task<AttachmentInfoDto?> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _attachmentRepository.GetInfoByIdAsync(id, cancellationToken);
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
            /*var curUser = _userRepository.GetCurrentUserAsync().Result;
            if (curUser == null)
            {
                return Task.FromResult(Guid.Empty);
            }*/

            var entity = new Domain.Attachment
            {
                Name = attachment.Name,
                Content = attachment.Content,
                ContentType = attachment.ContentType,
                Created = DateTime.UtcNow,
                Length = attachment.Content.Length,
                //Author = curUser,
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
