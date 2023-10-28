using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Domain;
using BulletinBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly IRepository<Attachment> _attachmentRepository;
        private readonly IRepository<Post> _postRepository;

        public AttachmentRepository(IRepository<Attachment> attachmentRepository, IRepository<Post> postRepository)
        {
            _attachmentRepository = attachmentRepository;
            _postRepository = postRepository;
        }

        ///<inheritdoc/>
        public Task<AttachmentInfoDto?> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() => _attachmentRepository.GetAll().Where(a => a.Id == id).Select(a => new AttachmentInfoDto { 
                Id = a.Id,
                Name = a.Name,
                ContentType = a.ContentType,
                Length = a.Length,
                Created = a.Created,
                PostId = a.Post.Id,
            }).FirstOrDefault(), cancellationToken);
        }

        ///<inheritdoc/>
        public Task<IEnumerable<AttachmentInfoDto>> GetAllInfoAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => _attachmentRepository.GetAll().Select(a => new AttachmentInfoDto
            {
                Id = a.Id,
                Name = a.Name,
                ContentType = a.ContentType,
                Length = a.Length,
                Created = a.Created,
                PostId = a.Post.Id,
            }).AsEnumerable(), cancellationToken);
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _attachmentRepository.GetByIdAsync(id, cancellationToken);
            if (entity == null)
            {
                return;
            }

            await _attachmentRepository.DeleteAsync(entity, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<Guid> UploadAsync(AttachmentDto attachment, Guid postId, CancellationToken cancellationToken)
        {
            var entity = new Attachment
            {
                Name = attachment.Name,
                Content = attachment.Content,
                ContentType = attachment.ContentType,
                Created = DateTime.UtcNow,
                Length = attachment.Content.Length,
            };

            var post = _postRepository.GetByIdAsync(postId, cancellationToken).Result;
            if (post == null)
            {
                return Guid.Empty;
            }
            entity.Post = post;

            await _attachmentRepository.AddAsync(entity, cancellationToken);
            return entity.Id;
        }

        ///<inheritdoc/>
        public Task<AttachmentDto?> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            return _attachmentRepository.GetAll().Where(s => s.Id == id).Select(s => new AttachmentDto
            {
                Content = s.Content,
                ContentType = s.ContentType,
                Name = s.Name,
                PostId = s.Post.Id,
            }).FirstOrDefaultAsync(cancellationToken);

            /*var attachment = _attachmentRepository.GetByIdAsync(id).Result;
            return Task.Run(() =>  new AttachmentDto
            {
                Content = attachment.Content,
                ContentType = attachment.ContentType,
                Name = attachment.Name,
            });*/
        }
    }
}
