using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Contracts.Attachment;
using System.Reflection;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Attachment.Repositories
{
    /// <inheritdoc/>
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly List<Domain.Attachments.Attachment> _attachments = new();

        /// <inheritdoc/>
        public Task<AttachmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() => new AttachmentDto
            {
                Id = Guid.NewGuid(),
                Title = "Test title",
                /*Data = */
            }, cancellationToken);        
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(Domain.Attachments.Attachment model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            _attachments.Add(model);
            return Task.Run(() => model.Id);
        }
    }
}
