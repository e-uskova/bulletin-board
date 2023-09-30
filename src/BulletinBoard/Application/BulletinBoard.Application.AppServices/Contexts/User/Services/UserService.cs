using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Domain;
using System.Linq.Expressions;
using System.Threading;

namespace BulletinBoard.Application.AppServices.Contexts.User.Services
{
    /// <inheritdoc/>
    public class UserService : IUserService
    {
        // private readonly IUserRepository _userRepository;
        private readonly IRepository<Domain.User> _userRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="UserService"/>
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        public UserService(IRepository<Domain.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = _userRepository.GetAllAsync().Result;
            IEnumerable<UserDto> result = new List<UserDto>();
            foreach (var user in users)
            {
                result = result.Append(new UserDto()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                });
            }
            return Task.Run(() => result);
        }

        public Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = _userRepository.GetByIdAsync(id).Result;
            return Task.Run(() => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            });
        }

        public Task<UserDto> GetFirstWhere(Expression<Func<Domain.User, bool>> predicate)
        {
            var user = _userRepository.GetFirstWhere(predicate).Result;
            return Task.Run(() => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            });
        }

        public Task<IEnumerable<UserDto>> GetRangeByIDAsync(List<Guid> ids)
        {
            var users = _userRepository.GetRangeByIDAsync(ids).Result;
            IEnumerable<UserDto> result = new List<UserDto>();
            foreach (var user in users)
            {
                result = result.Append(new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                });
            }
            return Task.Run(() => result);
        }

        public Task<IEnumerable<UserDto>> GetWhere(Expression<Func<Domain.User, bool>> predicate)
        {
            var users = _userRepository.GetWhere(predicate).Result;
            IEnumerable<UserDto> result = new List<UserDto>();
            foreach (var user in users)
            {
                result = result.Append(new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                });
            }
            return Task.Run(() => result);
        }

        public Task<Guid> AddAsync(CreateUserDto user)
        {
            Domain.User entity = new()
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
            return _userRepository.AddAsync(entity);
        }

        public Task UpdateAsync(UserDto user)
        {
            Domain.User entity = new()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
            return _userRepository.UpdateAsync(entity);
        }

        public Task DeleteAsync(UserDto user)
        {
            Domain.User entity = new()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
            return _userRepository.DeleteAsync(entity);
        }
    }
}