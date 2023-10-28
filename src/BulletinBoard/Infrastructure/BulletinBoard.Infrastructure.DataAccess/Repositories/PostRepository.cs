using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Domain;
using BulletinBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class PostRepository : IPostRepository
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Attachment> _attachmentRepository;

        public PostRepository(
            IRepository<Post> postRepository,
            IRepository<Category> categoryRepository,
            IRepository<User> userRepository,
            IRepository<Attachment> attachmentRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _attachmentRepository = attachmentRepository;
        }

        public Task<IEnumerable<PostDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize, int pageIndex)
        {
            var posts = _postRepository.GetAll().Skip(pageIndex * pageSize).Take(pageSize).ToListAsync().Result;
            if (posts == null)
            {
                return Task.FromResult<IEnumerable<PostDto>?>(null);
            }
            var result = (from post in posts
                          select Mapper.ToPostDto(post)).AsEnumerable();
            return Task.Run(() => result);
        }

        public Task<PostDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var post = _postRepository.GetByIdAsync(id, cancellationToken).Result;
            if (post == null)
            {
                return Task.FromResult<PostDto?>(null);
            }
            return Task.Run(() => Mapper.ToPostDto(post));
        }

        public Task<PostDto> GetFirstWhere(Expression<Func<Post, bool>> predicate, CancellationToken cancellationToken)
        {
            var post = _postRepository.GetFirstWhere(predicate, cancellationToken).Result;
            if (post == null)
            {
                return Task.FromResult<PostDto?>(null);
            }
            return Task.Run(() => Mapper.ToPostDto(post));
        }

        public Task<IEnumerable<PostDto>> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            var posts = _postRepository.GetRangeByIDAsync(ids, cancellationToken).Result;
            IEnumerable<PostDto> result = new List<PostDto>();
            foreach (var post in posts)
            {
                result = result.Append(Mapper.ToPostDto(post));
            }
            return Task.Run(() => result);
        }

        public Task<IEnumerable<PostDto>> GetWhere(Expression<Func<Post, bool>> predicate)
        {
            var posts = _postRepository.GetWhere(predicate).AsEnumerable();
            IEnumerable<PostDto> result = new List<PostDto>();
            foreach (var post in posts)
            {
                result = result.Append(Mapper.ToPostDto(post));
            }
            return Task.Run(() => result);
        }

        public Task<Guid> AddAsync(CreatePostDto post, UserDto curUser, CancellationToken cancellationToken)
        {
            Post entity = new()
            {
                Id = Guid.NewGuid(),
                Title = post.Title,
                Description = post.Description,
                Price = post.Price,
                Author = _userRepository.GetByIdAsync(curUser.Id, cancellationToken).Result,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                IsActive = true,
            };
            var category = _categoryRepository.GetByIdAsync(post.CategoryId, cancellationToken).Result;
            if (category == null)
            {
                return Task.FromResult(Guid.Empty);
            }
            entity.Category = category;

            return _postRepository.AddAsync(entity, cancellationToken);
        }

        public Task<bool> UpdateAsync(Guid id, CreatePostDto post, CancellationToken cancellationToken)
        {
            var existedPost = _postRepository.GetByIdAsync(id, cancellationToken);
            if (existedPost == null)
            {
                return Task.Run(() => true);
            }

            Post entity = existedPost.Result;
            entity.Title = post.Title;
            entity.Description = post.Description;
            entity.Price = post.Price;
            entity.Category = _categoryRepository.GetByIdAsync(post.CategoryId, cancellationToken).Result;
            entity.Modified = DateTime.UtcNow;

            _postRepository.UpdateAsync(entity, cancellationToken);
            return Task.Run(() => false);
        }

        public Task CloseAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedPost = _postRepository.GetByIdAsync(id, cancellationToken);
            if (existedPost == null)
            {
                return Task.Run(() => true);
            }

            Post entity = existedPost.Result;
            entity.IsActive = false;

            _postRepository.UpdateAsync(entity, cancellationToken);
            return Task.Run(() => false);
        }

        public Task ReOpenAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedPost = _postRepository.GetByIdAsync(id, cancellationToken);
            if (existedPost == null)
            {
                return Task.Run(() => true);
            }

            Post entity = existedPost.Result;
            entity.IsActive = true;

            _postRepository.UpdateAsync(entity, cancellationToken);
            return Task.Run(() => false);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedPost = _postRepository.GetByIdAsync(id, cancellationToken);
            if (existedPost == null)
            {
                return Task.Run(() => true);
            }

            _postRepository.DeleteAsync(existedPost.Result, cancellationToken);
            return Task.Run(() => false);
        }

    }
}
