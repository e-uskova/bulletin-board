using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Domain;
using System.Reflection;

namespace BulletinBoard.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly List<Attachment> _attachments = new();

        /// <inheritdoc/>
        public Task<AttachmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() => new AttachmentDto
            {
                Id = Guid.NewGuid(),
                Name = "Test title",
                /*Data = */
            }, cancellationToken);        
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(Attachment model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            _attachments.Add(model);
            return Task.Run(() => model.Id);
        }
    }
}
