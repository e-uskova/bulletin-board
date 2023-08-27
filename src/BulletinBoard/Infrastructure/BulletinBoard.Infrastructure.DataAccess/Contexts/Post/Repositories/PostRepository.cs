using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Contracts;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Post.Repositories
{
    public class PostRepository : IPostRepository
    {
        public Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() => new PostDto
            {
                Id = Guid.NewGuid(),
                Title = "Test title",
                Description = "Opisanie",
                CategoryName = "testtt",
                TagNames = new []{ "first", "last" },
                Price = 500.43M
            }, cancellationToken);
        }
    }
}
