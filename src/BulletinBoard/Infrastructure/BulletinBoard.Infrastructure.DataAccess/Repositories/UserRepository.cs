using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Infrastructure.DataAccess.Data;

namespace BulletinBoard.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class UserRepository : IUserRepository
    {
        //private readonly List<Domain.User> _users = new();
        private readonly List<Domain.User> _users = FakeDataFactory.Users;

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(Domain.User model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            _users.Add(model);
            return Task.Run(() => model.Id);
        }

        /// <inheritdoc/>
        public Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                user = _users[0];
            }
            return Task.Run(() => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            }, cancellationToken);
        }
    }
}
