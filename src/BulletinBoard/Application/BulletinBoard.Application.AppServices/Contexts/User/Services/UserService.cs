using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
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

        public Task<UserDto?> GetByIdAsync(Guid id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<UserDto> GetFirstWhere(Expression<Func<Domain.User, bool>> predicate)
        {
            return _userRepository.GetFirstWhere(predicate);
        }

        public Task<IEnumerable<UserDto>> GetRangeByIDAsync(List<Guid> ids)
        {
            return _userRepository.GetRangeByIDAsync(ids);
        }

        public Task<IEnumerable<UserDto>> GetWhere(Expression<Func<Domain.User, bool>> predicate)
        {
            return _userRepository.GetWhere(predicate);
        }

        public Task<Guid> AddAsync(CreateUserDto user)
        {
            return _userRepository.AddAsync(user);
        }

        public Task<bool> UpdateAsync(Guid id, CreateUserDto user)
        {
            return _userRepository.UpdateAsync(id, user);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _userRepository.DeleteAsync(id);
        }
    }
}