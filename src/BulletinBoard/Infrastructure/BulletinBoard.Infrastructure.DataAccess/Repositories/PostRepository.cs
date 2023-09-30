using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Domain;

namespace BulletinBoard.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class PostRepository : IPostRepository
    {
        private readonly List<Post> _posts = new();

        /// <inheritdoc/>
        public Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() => new PostDto
            {
                Id = Guid.NewGuid(),
                Title = "Test title",
                Description = "Opisanie",
                CategoryName = "testtt",
                Price = 500.43M
            }, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(Post model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();  
            _posts.Add(model);
            return Task.Run(() => model.Id);
        }

    }
}
