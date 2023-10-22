using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Domain;
using System.Linq.Expressions;

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

        public Task<Guid> AddAsync(CreatePostDto post, UserDto curUser)
        {
            return _postRepository.AddAsync(post, curUser);
        }

        public Task<bool> UpdateAsync(Guid id, CreatePostDto post)
        {
            return _postRepository.UpdateAsync(id, post);
        }

        public Task CloseAsync(Guid id)
        {
            return _postRepository.CloseAsync(id);
        }

        public Task ReOpenAsync(Guid id)
        {
            return _postRepository.ReOpenAsync(id);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _postRepository.DeleteAsync(id);
        }
    }
}
