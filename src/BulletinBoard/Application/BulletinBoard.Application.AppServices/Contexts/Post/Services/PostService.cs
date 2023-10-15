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
        private readonly IRepository<Domain.User> _userRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="PostService"/>
        /// </summary>
        /// <param name="postRepository">Репозиторий для работы с объявлениями.</param>
        public PostService(
            IRepository<Domain.Post> postRepository, 
            IRepository<Domain.Category> categoryRepository, 
            IRepository<Domain.User> userRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository; 
            _userRepository = userRepository;
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
                Category = _categoryRepository.GetByIdAsync(post.CategoryId).Result
            };
            var currentUser = _userRepository.GetByIdAsync(Guid.Parse("9f3c1162-8a98-4e80-9277-e6f56bbc5161")).Result; // TODO получать id текущего пользователя
            if (currentUser != null)
            {
                if (currentUser.Posts == null)
                {
                    currentUser.Posts = new List<Domain.Post> { entity };
                }
                else
                {
                    currentUser.Posts = (IReadOnlyCollection<Domain.Post>)currentUser.Posts.Append(entity);
                }
                //_userRepository.UpdateAsync(currentUser);
            }
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
