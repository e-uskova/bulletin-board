using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Contracts.User;
using BulletinBoard.Contracts.Users;
using System.Linq.Expressions;
using System.Threading;

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

        public Task<IEnumerable<UserDto>?> GetAllAsync(CancellationToken cancellationToken)
        {
            return _userRepository.GetAllAsync(cancellationToken);
        }

        public Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<UserDto?> GetFirstWhere(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken)
        {
            return _userRepository.GetFirstWhere(predicate, cancellationToken);
        }

        public async Task<Guid> AddAsync(CreateUserDto user, CancellationToken cancellationToken)
        {
            var existedUser = await _userRepository.GetFirstWhere(u => u.Email == user.Email, cancellationToken);
            if (existedUser != null)
            {
                return Guid.Empty;
            }

            return await _userRepository.AddAsync(user, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Guid id, EditUserDto user, CancellationToken cancellationToken)
        {
            var existedUser = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (existedUser == null)
            {
                return true;
            }

            return await _userRepository.UpdateAsync(id, user, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedUser = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (existedUser == null)
            {
                return true;
            }
            return await _userRepository.DeleteAsync(id, cancellationToken);
        }
    }
}