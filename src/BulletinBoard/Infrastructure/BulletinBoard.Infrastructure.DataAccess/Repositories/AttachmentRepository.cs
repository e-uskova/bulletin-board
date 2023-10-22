using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Domain;
using Microsoft.EntityFrameworkCore;
//using System.Net.Mail;

namespace BulletinBoard.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly IRepository<Attachment> _attachmentRepository;

        public AttachmentRepository(IRepository<Attachment> attachmentRepository )
        {
            _attachmentRepository = attachmentRepository;
        }

        ///<inheritdoc/>
        public Task<AttachmentInfoDto?> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() => _attachmentRepository.GetAllAsync().Where(a => a.Id == id).Select(a => new AttachmentInfoDto { 
                Id = a.Id,
                Name = a.Name,
                ContentType = a.ContentType,
                Length = a.Length,
                Created = a.Created,
            }).FirstOrDefault());
        }

        ///<inheritdoc/>
        public Task<IEnumerable<AttachmentInfoDto>> GetAllInfoAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => _attachmentRepository.GetAllAsync().Select(a => new AttachmentInfoDto
            {
                Id = a.Id,
                Name = a.Name,
                ContentType = a.ContentType,
                Length = a.Length,
                Created = a.Created,
            }).AsEnumerable());
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _attachmentRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return;
            }

            await _attachmentRepository.DeleteAsync(entity);
        }

        ///<inheritdoc/>
        public async Task<Guid> UploadAsync(Attachment attachment, CancellationToken cancellationToken)
        {
            await _attachmentRepository.AddAsync( attachment );
            return attachment.Id;
        }

        ///<inheritdoc/>
        public Task<AttachmentDto?> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            return _attachmentRepository.GetAllAsync().Where(s => s.Id == id).Select(s => new AttachmentDto
            {
                Content = s.Content,
                ContentType = s.ContentType,
                Name = s.Name,
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
