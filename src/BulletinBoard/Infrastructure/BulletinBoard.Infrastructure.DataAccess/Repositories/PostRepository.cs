﻿using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Http;
using System.Security.Claims;

namespace BulletinBoard.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class PostRepository : IPostRepository
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<User> _userRepository;

        public PostRepository(IRepository<Post> postRepository, IRepository<Category> categoryRepository, IRepository<User> userRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
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

        public Task<PostDto> GetFirstWhere(Expression<Func<Post, bool>> predicate)
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

        public Task<Guid> AddAsync(CreatePostDto post, UserDto curUser)
        {
            Post entity = new()
            {
                Id = Guid.NewGuid(),
                Title = post.Title,
                Description = post.Description,
                Price = post.Price,
                Category = _categoryRepository.GetByIdAsync(post.CategoryId).Result,
                Author = _userRepository.GetByIdAsync(curUser.Id).Result
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

            Post entity = existedPost.Result;
            entity.Title = post.Title;
            entity.Description = post.Description;
            entity.Price = post.Price;
            entity.Category = _categoryRepository.GetByIdAsync(post.CategoryId).Result;

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
