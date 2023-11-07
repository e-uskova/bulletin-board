using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Contracts.Users;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Post.Services
{
    /// <inheritdoc/>
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="PostService"
        /// </summary>
        /// <param name="postRepository">Репозиторий для работы с объявлениями.</param>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        /// <param name="categoryRepository">Репозиторий для работы с категориями.</param>
        public PostService(IPostRepository postRepository,
                           IUserRepository userRepository,
                           ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<PostDto>?> GetAllAsync(CancellationToken cancellationToken, int pageSize, int pageIndex)
        {
            return _postRepository.GetAllAsync(cancellationToken, pageSize, pageIndex);
        }

        public Task<PostDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _postRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<PostDto?> GetFirstWhere(Expression<Func<Domain.Post, bool>> predicate, CancellationToken cancellationToken)
        {
            return _postRepository.GetFirstWhere(predicate, cancellationToken);
        }

        public Task<IEnumerable<PostDto>?> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            return _postRepository.GetRangeByIDAsync(ids, cancellationToken);
        }

        public Task<IEnumerable<PostDto>?> GetWhere(Expression<Func<Domain.Post, bool>> predicate, CancellationToken cancellationToken)
        {
            return _postRepository.GetWhere(predicate, cancellationToken);
        }

        public async Task<Guid> AddAsync(CreatePostDto post, UserDto curUser, CancellationToken cancellationToken)
        {
            var author = await _userRepository.GetByIdAsync(curUser.Id, cancellationToken);
            if (author == null)
            {
                return Guid.Empty;
            }

            var category = await _categoryRepository.GetByIdAsync(post.CategoryId, cancellationToken);
            if (category == null)
            {
                return Guid.Empty;
            }

            return await _postRepository.AddAsync(post, curUser, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Guid id, EditPostDto post, CancellationToken cancellationToken)
        {
            var existedPost = await _postRepository.GetByIdAsync(id, cancellationToken);
            if (existedPost == null)
            {
                return true;
            }
            if (post.CategoryId != null)
            {
                var category = await _categoryRepository.GetByIdAsync((Guid)post.CategoryId, cancellationToken);
                if (category == null)
                {
                    return true;
                }
            }

            return await _postRepository.UpdateAsync(id, post, cancellationToken);
        }

        public async Task<bool> CloseAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedPost = await _postRepository.GetByIdAsync(id, cancellationToken);
            if (existedPost == null)
            {
                return true;
            }

            return await _postRepository.CloseAsync(id, cancellationToken);
        }

        public async Task<bool> ReOpenAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedPost = await _postRepository.GetByIdAsync(id, cancellationToken);
            if (existedPost == null)
            {
                return true;
            }

            return await _postRepository.ReOpenAsync(id, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedPost = await _postRepository.GetByIdAsync(id, cancellationToken);
            if (existedPost == null)
            {
                return true;
            }

            return await _postRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
