using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Contracts.User;
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

        public PostRepository(
            IRepository<Post> postRepository,
            IRepository<Category> categoryRepository,
            IRepository<User> userRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<PostDto>?> GetAllAsync(CancellationToken cancellationToken, int pageSize, int pageIndex)
        {
            var posts = await _postRepository.GetAll().Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            if (posts == null)
            {
                return null;
            }
            var result = (from post in posts
                          select Mapper.ToPostDto(post)).AsEnumerable();
            return result;
        }

        public async Task<PostDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(id, cancellationToken);
            return Mapper.ToPostDto(post);
        }

        public async Task<PostDto?> GetFirstWhere(Expression<Func<Post, bool>> predicate, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetFirstWhere(predicate, cancellationToken);
            return Mapper.ToPostDto(post);
        }

        public async Task<IEnumerable<PostDto>?> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetRangeByIDAsync(ids, cancellationToken);
            if (posts == null)
            {
                return null;
            }
            var result = (from post in posts
                          select Mapper.ToPostDto(post)).AsEnumerable();
            return result;
        }

        public async Task<IEnumerable<PostDto>?> GetWhere(Expression<Func<Post, bool>> predicate, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetWhere(predicate).ToListAsync(cancellationToken);
            if (posts == null)
            {
                return null;
            }
            var result = (from post in posts
                          select Mapper.ToPostDto(post)).AsEnumerable();
            return result;
        }

        public async Task<Guid> AddAsync(CreatePostDto post, UserDto curUser, CancellationToken cancellationToken)
        {
            Post entity = new()
            {
                Id = Guid.NewGuid(),
                Title = post.Title,
                Description = post.Description,
                Price = post.Price,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                IsActive = true,
                Author = await _userRepository.GetByIdAsync(curUser.Id, cancellationToken),
                Category = await _categoryRepository.GetByIdAsync(post.CategoryId, cancellationToken)
            };

            return await _postRepository.AddAsync(entity, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Guid id, EditPostDto post, CancellationToken cancellationToken)
        {
            var entity = await _postRepository.GetByIdAsync(id, cancellationToken);

            if (post.Title != null)
            {
                entity.Title = post.Title;
            }
            if (post.Description != null)
            {
                entity.Description = post.Description;
            }
            if (post.Price != null)
            {
                entity.Price = (decimal)post.Price;    
            }
            if (post.CategoryId != null)
            {
                entity.Category = await _categoryRepository.GetByIdAsync((Guid)post.CategoryId, cancellationToken);
                
            }
            entity.Modified = DateTime.UtcNow;

            await _postRepository.UpdateAsync(entity, cancellationToken);
            return false;
        }

        public async Task<bool> CloseAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _postRepository.GetByIdAsync(id, cancellationToken);

            entity.IsActive = false;
            entity.Modified = DateTime.UtcNow;

            await _postRepository.UpdateAsync(entity, cancellationToken);
            return false;
        }

        public async Task<bool> ReOpenAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _postRepository.GetByIdAsync(id, cancellationToken);

            entity.IsActive = true;
            entity.Modified = DateTime.UtcNow;

            await _postRepository.UpdateAsync(entity, cancellationToken);
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedPost = await _postRepository.GetByIdAsync(id, cancellationToken);

            await _postRepository.DeleteAsync(existedPost, cancellationToken);
            return false;
        }

        public async Task ModifyAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _postRepository.GetByIdAsync(id, cancellationToken);

            entity.Modified = DateTime.UtcNow;

            await _postRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
