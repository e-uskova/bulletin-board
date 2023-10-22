﻿using BulletinBoard.Application.AppServices.Contexts.Post.Services;
using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Contracts.Categories;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Domain;

namespace BulletinBoard.Application.AppServices.Mapping
{
    public static class Mapper
    {
        public static UserDto ToUserDto(User user)
        {
            var posts = new List<PostDto>();
            if (user.Posts != null)
            {
                foreach(Post post in user.Posts)
                {
                    posts.Add(ToPostDto(post));
                }
            }
            
            return new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Posts = posts,
                Telephone = user.Telephone,
            };
        }

        public static PostDto ToPostDto(Post post)
        {
            var result = new PostDto()
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                CategoryName = post.Category?.Name,
                Price = post.Price,
                Created = post.Created,
                Modified = post.Modified,
                IsActive = post.IsActive,
                // TODO Attachments
            };
            if (post.Author != null)
            {
                result.Author = ToUserDto(post.Author);

            }
            return result;
        }

        public static CategoryDto ToCategoryDto(Category category)
        {
            return new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryId = category.ParentCategory?.Id,
            };
        }

        public static AttachmentDto ToAttachmentDto(Attachment attachment)
        {
            return new AttachmentDto()
            {
                Id = attachment.Id,
                Name = attachment.Name,
                Content = attachment.Content,
                ContentType = attachment.ContentType,
            };
        }
    }
}