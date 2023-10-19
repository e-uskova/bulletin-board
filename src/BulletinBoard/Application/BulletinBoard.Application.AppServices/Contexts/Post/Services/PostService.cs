using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Contracts.Post;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Post.Services
{
    /// <inheritdoc/>
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="PostService"/>
        /// </summary>
        /// <param name="postRepository">Репозиторий для работы с объявлениями.</param>
        public PostService(
            IPostRepository postRepository,
            ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository; 
        }

        public Task<IEnumerable<PostDto>> GetAllAsync()
        {
            return _postRepository.GetAllAsync();
        }

        public Task<PostDto?> GetByIdAsync(Guid id)
        {
            return _postRepository.GetByIdAsync(id);
        }

        public Task<PostDto> GetFirstWhere(Expression<Func<Domain.Post, bool>> predicate)
        {
            return _postRepository.GetFirstWhere(predicate);
        }

        public Task<IEnumerable<PostDto>> GetRangeByIDAsync(List<Guid> ids)
        {
            return _postRepository.GetRangeByIDAsync(ids);
        }

        public Task<IEnumerable<PostDto>> GetWhere(Expression<Func<Domain.Post, bool>> predicate)
        {
            return _postRepository.GetWhere(predicate);
        }

        public Task<Guid> AddAsync(CreatePostDto post)
        {
            return _postRepository.AddAsync(post);
        }

        public Task<bool> UpdateAsync(Guid id, CreatePostDto post)
        {
            return _postRepository.UpdateAsync(id, post);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _postRepository.DeleteAsync(id);
        }
    }
}
