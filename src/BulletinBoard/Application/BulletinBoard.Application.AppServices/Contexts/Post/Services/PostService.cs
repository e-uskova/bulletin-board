using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Contracts;

namespace BulletinBoard.Application.AppServices.Contexts.Post.Services
{
    /// <inheritdoc/>
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        /*private readonly ICategoryRepository _categoryRepository;
        private readonly IAttachmentRepository _attachmentRepository;*/

        /// <summary>
        /// Инициализация экземпляра <see cref="PostService"/>
        /// </summary>
        /// <param name="postRepository">Репозиторий для работы с объявлениями.</param>
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        /// <inheritdoc/>
        public Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _postRepository.GetByIdAsync(id, cancellationToken);
        }
    }
}
