using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Domain;

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
        public async Task<Guid> UploadAsync(Attachment attachment, CancellationToken cancellationToken)
        {
            await _attachmentRepository.AddAsync( attachment );
            return attachment.Id;
        }

        ///<inheritdoc/>
        public Task<AttachmentDto> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            /*return _attachmentRepository.GetWhere(s => s.Id == id).Select(s => new AttachmentDto
            {
                Content = s.Content,
                ContentType = s.ContentType,
                Name = s.Name,
            }).FirstOrDefaultAsync(cancellationToken);*/

            var attachment = _attachmentRepository.GetByIdAsync(id).Result;
            return Task.Run(() =>  new AttachmentDto
            {
                Content = attachment.Content,
                ContentType = attachment.ContentType,
                Name = attachment.Name,
            });
        }
    }
}
