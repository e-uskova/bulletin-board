using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Domain;

namespace BulletinBoard.Application.AppServices.Contexts.Attachment.Services
{
    /// <inheritdoc/>
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IPostRepository _postRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="AttachmentService"/>
        /// </summary>
        /// <param name="attachmentRepository">Репозиторий для работы с вложениями.</param>
        public AttachmentService(IAttachmentRepository attachmentRepository, IPostRepository postRepository)
        {
            _attachmentRepository = attachmentRepository;
            _postRepository = postRepository;
        }

        /// <inheritdoc/>
        public Task<AttachmentInfoDto?> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _attachmentRepository.GetInfoByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedEntity = await _attachmentRepository.GetInfoByIdAsync(id, cancellationToken);
            if (existedEntity == null) 
            {
                return true;
            }

            await _attachmentRepository.DeleteAsync(id, cancellationToken);

            var post = await _postRepository.GetByIdAsync(existedEntity.PostId, cancellationToken);
            if (post != null)
            {
                await _postRepository.ModifyAsync(post.Id, cancellationToken);
            }

            return false;
        }

        /// <inheritdoc/>
        public async Task<Guid> UploadAsync(AttachmentDto attachment, Guid postId, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(postId, cancellationToken);
            if (post == null)
            {
                return Guid.Empty;
            }

            if (post.Attachments.Count >= 10)
            {
                return Guid.Empty;
            }

            var result = await _attachmentRepository.UploadAsync(attachment, postId, cancellationToken);
            await _postRepository.ModifyAsync(post.Id, cancellationToken);

            return result;
        }

        /// <inheritdoc/>
        public async Task<AttachmentDto?> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedEntity = await _attachmentRepository.GetInfoByIdAsync(id, cancellationToken);
            if (existedEntity == null)
            {
                return null;
            }

            return await _attachmentRepository.DownloadAsync(id, cancellationToken);
        }
    }
}
