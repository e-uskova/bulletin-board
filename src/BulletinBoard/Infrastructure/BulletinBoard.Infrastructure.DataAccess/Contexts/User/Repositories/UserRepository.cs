using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Contracts.Users;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.User.Repositories
{
    /// <inheritdoc/>
    public class UserRepository : IUserRepository
    {
        private readonly List<Domain.Users.User> _users = new();

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(Domain.Users.User model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            _users.Add(model);
            return Task.Run(() => model.Id);
        }

        /// <inheritdoc/>
        public Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() => new UserDto
            {
                Id = Guid.NewGuid(),
                UserName = "John Doe",
                UserEmail = "jd@example.com",
                Password = "1234"
            }, cancellationToken);
        }
    }
}
