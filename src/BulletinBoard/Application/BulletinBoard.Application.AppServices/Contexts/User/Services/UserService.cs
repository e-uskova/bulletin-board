using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
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
                result = result.Append(Mapper.ToUserDto(user));
            }
            return Task.Run(() => result);
        }

        public Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = _userRepository.GetByIdAsync(id).Result;
            if (user == null)
            {
                return Task.FromResult<UserDto?>(null);
            }
            return Task.Run(() => Mapper.ToUserDto(user));
        }

        public Task<UserDto> GetFirstWhere(Expression<Func<Domain.User, bool>> predicate)
        {
            var user = _userRepository.GetFirstWhere(predicate).Result;
            return Task.Run(() => Mapper.ToUserDto(user));
        }

        public Task<IEnumerable<UserDto>> GetRangeByIDAsync(List<Guid> ids)
        {
            var users = _userRepository.GetRangeByIDAsync(ids).Result;
            IEnumerable<UserDto> result = new List<UserDto>();
            foreach (var user in users)
            {
                result = result.Append(Mapper.ToUserDto(user));
            }
            return Task.Run(() => result);
        }

        public Task<IEnumerable<UserDto>> GetWhere(Expression<Func<Domain.User, bool>> predicate)
        {
            var users = _userRepository.GetWhere(predicate).Result;
            IEnumerable<UserDto> result = new List<UserDto>();
            foreach (var user in users)
            {
                result = result.Append(Mapper.ToUserDto(user));
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
                Password = user.Password
            };
            return _userRepository.AddAsync(entity);
        }

        public Task<bool> UpdateAsync(Guid id, CreateUserDto user)
        {
            //Domain.User entity = _userRepository.GetByIdAsync(user.Id).Result;

            var existedUser = _userRepository.GetByIdAsync(id);
            if (existedUser == null)
            {
                return Task.Run(() => true);
            }

            Domain.User entity = existedUser.Result;
            entity.Name = user.Name;
            entity.Email = user.Email;  
            entity.Password = user.Password;


            /*Domain.User entity = new()
            {
                //Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };*/
            _userRepository.UpdateAsync(entity);
            return Task.Run(() => false);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var existedUser = _userRepository.GetByIdAsync(id);
            if (existedUser == null)
            {
                return Task.Run(() => true);
            }

            _userRepository.DeleteAsync(existedUser.Result);
            return Task.Run(() => false);
        }
    }
}