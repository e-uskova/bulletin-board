using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Contracts.Users;

namespace BulletinBoard.Application.AppServices.Contexts.User.Services
{
    /// <inheritdoc/>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="UserService"/>
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc/>
        public Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(CreateUserDto model, CancellationToken cancellationToken)
        {
            var user = new Domain.Users.User()
            {
                UserName = model.UserName,
                UserEmail = model.UserEmail,
                Password = model.Password,
            };
            return _userRepository.CreateAsync(user, cancellationToken);
        }

    }
}
