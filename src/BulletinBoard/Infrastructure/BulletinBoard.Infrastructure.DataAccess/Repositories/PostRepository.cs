using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class PostRepository : IPostRepository
    {
        private readonly IRepository<Post> _postRepository;

        public PostRepository(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public Task<IEnumerable<PostDto>> GetAllAsync()
        {
            var posts = _postRepository.GetAllAsync().ToListAsync().Result;
            if (posts == null)
            {
                return Task.FromResult<IEnumerable<PostDto>?>(null);
            }
            var result = (from post in posts
                          select Mapper.ToPostDto(post)).AsEnumerable();
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
            if (post == null)
            {
                return Task.FromResult<PostDto?>(null);
            }
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
            var posts = _postRepository.GetWhere(predicate).AsEnumerable();
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
                /*Name = post.Name,
                Email = post.Email,
                Password = post.Password,
                Telephone = post.Telephone,*/
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
            /*entity.Name = post.Name;
            entity.Email = post.Email;
            entity.Password = post.Password;
            entity.Telephone = post.Telephone;*/

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
