using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.User.Services
{
    /// <inheritdoc/>
    public class UserService : IUserService
    {
        // private readonly IUserRepository _userRepository;
        private readonly IRepository<Domain.Users.User> _userRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="UserService"/>
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        public UserService(IRepository<Domain.Users.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IEnumerable<Domain.Users.User>> GetAllAsync()
        {            
            return  _userRepository.GetAllAsync();
        }

        public Task<Domain.Users.User> GetByIdAsync(Guid id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<Domain.Users.User> GetFirstWhere(Expression<Func<Domain.Users.User, bool>> predicate)
        {
            return _userRepository.GetFirstWhere(predicate);
        }

        public Task<IEnumerable<Domain.Users.User>> GetRangeByIDAsync(List<Guid> ids)
        {
            return _userRepository.GetRangeByIDAsync(ids);
        }

        public Task<IEnumerable<Domain.Users.User>> GetWhere(Expression<Func<Domain.Users.User, bool>> predicate)
        {
            return _userRepository.GetWhere(predicate);
        }

        public Task AddAsync(Domain.Users.User entity)
        {
            return _userRepository.AddAsync(entity);
        }

        public Task UpdateAsync(Domain.Users.User entity)
        {
            return _userRepository.UpdateAsync(entity);
        }

        public Task DeleteAsync(Domain.Users.User entity)
        {
            return _userRepository.DeleteAsync(entity);
        }
    }
}
