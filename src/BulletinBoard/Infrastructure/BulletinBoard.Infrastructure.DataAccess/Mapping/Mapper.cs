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
            };
            if (post.Author != null)
            {
                result.AuthorName = post.Author.Name;
            }
            if (post.Attachments != null)
            {
                var attachments = new List<AttachmentInfoDto>();
                foreach(var attachment in post.Attachments)
                {
                    attachments.Add(ToAttachmentInfoDto(attachment));
                }
                result.Attachments = attachments;
            }

            return result;
        }

        public static CategoryDto? ToCategoryDto(Category? category)
        {
            if (category == null) 
            { 
                return null;
            }
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

        public static AttachmentInfoDto ToAttachmentInfoDto(Attachment attachment)
        {
            return new AttachmentInfoDto()
            {
                Id = attachment.Id,
                Name = attachment.Name,
                ContentType = attachment.ContentType,
                Length = attachment.Length,
                Created = attachment.Created,
                PostId = attachment.Post.Id
            };
        }
    }
}
