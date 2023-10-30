using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Contracts.User;
using BulletinBoard.Contracts.Users;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.User.Services
{
    /// <inheritdoc/>
    public class UserService : IUserService
    {
        // private readonly IUserRepository _userRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="UserService"/>
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<UserDto> GetFirstWhere(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken)
        {
            return _userRepository.GetFirstWhere(predicate, cancellationToken);
        }

        public Task<IEnumerable<UserDto>> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            return _userRepository.GetRangeByIDAsync(ids, cancellationToken);
        }

        public Task<IEnumerable<UserDto>> GetWhere(Expression<Func<Domain.User, bool>> predicate)
        {
            return _userRepository.GetWhere(predicate);
        }

        public Task<Guid> AddAsync(CreateUserDto user, CancellationToken cancellationToken)
        {
            return _userRepository.AddAsync(user, cancellationToken);
        }

        public Task<bool> UpdateAsync(Guid id, EditUserDto user, CancellationToken cancellationToken)
        {
            return _userRepository.UpdateAsync(id, user, cancellationToken);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userRepository.DeleteAsync(id, cancellationToken);
        }
    }
}