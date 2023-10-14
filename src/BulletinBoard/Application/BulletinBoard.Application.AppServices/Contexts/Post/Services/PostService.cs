using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.Post;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Post.Services
{
    /// <inheritdoc/>
    public class PostService : IPostService
    {
        private readonly IRepository<Domain.Post> _postRepository;
        private readonly IRepository<Domain.Category> _categoryRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="PostService"/>
        /// </summary>
        /// <param name="postRepository">Репозиторий для работы с объявлениями.</param>
        public PostService(IRepository<Domain.Post> postRepository, IRepository<Domain.Category> categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;   
        }

        public Task<IEnumerable<PostDto>> GetAllAsync()
        {
            var posts = _postRepository.GetAllAsync().Result;
            IEnumerable<PostDto> result = new List<PostDto>();
            foreach (var post in posts)
            {
                result = result.Append(Mapper.ToPostDto(post));
            }
            return Task.Run(() => result);
        }

        public Task<PostDto?> GetByIdAsync(Guid id)
        {
            var post = _postRepository.GetByIdAsync(id).Result;
            if (post == null)
            {
                return Task.FromResult<PostDto?>(null);
            }
            return Task.Run(() => Mapper.ToPostDto(post));
        }

        public Task<PostDto> GetFirstWhere(Expression<Func<Domain.Post, bool>> predicate)
        {
            var post = _postRepository.GetFirstWhere(predicate).Result;
            return Task.Run(() => Mapper.ToPostDto(post));
        }

        public Task<IEnumerable<PostDto>> GetRangeByIDAsync(List<Guid> ids)
        {
            var posts = _postRepository.GetRangeByIDAsync(ids).Result;
            IEnumerable<PostDto> result = new List<PostDto>();
            foreach (var post in posts)
            {
                result = result.Append(Mapper.ToPostDto(post));
            }
            return Task.Run(() => result);
        }

        public Task<IEnumerable<PostDto>> GetWhere(Expression<Func<Domain.Post, bool>> predicate)
        {
            var posts = _postRepository.GetWhere(predicate).Result;
            IEnumerable<PostDto> result = new List<PostDto>();
            foreach (var post in posts)
            {
                result = result.Append(Mapper.ToPostDto(post));
            }
            return Task.Run(() => result);
        }

        public Task<Guid> AddAsync(CreatePostDto post)
        {
            Domain.Post entity = new()
            {
                Id = Guid.NewGuid(),
                Title = post.Title,
                Description = post.Description,
                Price = post.Price,
                // TODO Category = _categoryRepository.GetByIdAsync(post.CategoryId)
            };
            return _postRepository.AddAsync(entity);
        }

        public Task<bool> UpdateAsync(Guid id, CreatePostDto post)
        {
            var existedPost = _postRepository.GetByIdAsync(id);
            if (existedPost == null)
            {
                return Task.Run(() => true);
            }

            Domain.Post entity = existedPost.Result;
            entity.Title = post.Title;
            entity.Description = post.Description;
            entity.Price = post.Price;
            // TODO Category

            _postRepository.UpdateAsync(entity);
            return Task.Run(() => false);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var existedPost = _postRepository.GetByIdAsync(id);
            if (existedPost == null)
            {
                return Task.Run(() => true);
            }

            _postRepository.DeleteAsync(existedPost.Result);
            return Task.Run(() => false);
        }
    }
}
