using BulletinBoard.Application.AppServices.Contexts.Post.Services;
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
                Posts = posts
            };
        }

        public static PostDto ToPostDto(Post post)
        {
            return new PostDto()
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                CategoryName = post.Category.Name,
                Price = post.Price,
            };
        }


    }
}
