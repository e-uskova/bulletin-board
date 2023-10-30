using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Domain;
using System.Linq.Expressions;
using System.Threading;

namespace BulletinBoard.Application.AppServices.Contexts.Post.Services
{
    /// <inheritdoc/>
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="PostService"/>
        /// </summary>
        /// <param name="postRepository">Репозиторий для работы с объявлениями.</param>
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Task<IEnumerable<PostDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize, int pageIndex)
        {
            return _postRepository.GetAllAsync(cancellationToken, pageSize, pageIndex);
        }

        public Task<PostDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _postRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<PostDto> GetFirstWhere(Expression<Func<Domain.Post, bool>> predicate, CancellationToken cancellationToken)
        {
            return _postRepository.GetFirstWhere(predicate, cancellationToken);
        }

        public Task<IEnumerable<PostDto>> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            return _postRepository.GetRangeByIDAsync(ids, cancellationToken);
        }

        public Task<IEnumerable<PostDto>> GetWhere(Expression<Func<Domain.Post, bool>> predicate)
        {
            return _postRepository.GetWhere(predicate);
        }

        public Task<Guid> AddAsync(CreatePostDto post, UserDto curUser, CancellationToken cancellationToken)
        {
            return _postRepository.AddAsync(post, curUser, cancellationToken);
        }

        public Task<bool> UpdateAsync(Guid id, EditPostDto post, CancellationToken cancellationToken)
        {
            return _postRepository.UpdateAsync(id, post, cancellationToken);
        }

        public Task CloseAsync(Guid id, CancellationToken cancellationToken)
        {
            return _postRepository.CloseAsync(id, cancellationToken);
        }

        public Task ReOpenAsync(Guid id, CancellationToken cancellationToken)
        {
            return _postRepository.ReOpenAsync(id, cancellationToken);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _postRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
