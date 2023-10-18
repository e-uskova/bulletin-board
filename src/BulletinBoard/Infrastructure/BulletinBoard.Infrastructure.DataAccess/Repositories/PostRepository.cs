using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Domain;

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

        

    }
}
