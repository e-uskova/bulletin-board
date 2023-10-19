using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _userRepository;

        public UserRepository(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = _userRepository.GetAllAsync().ToListAsync().Result;
            if (users == null)
            {
                return Task.FromResult<IEnumerable<UserDto>?>(null);
            }
            var result = (from user in users
                          select Mapper.ToUserDto(user)).AsEnumerable();
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
            if (user == null)
            {
                return Task.FromResult<UserDto?>(null);
            }
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
            var users = _userRepository.GetWhere(predicate).AsEnumerable();
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
                Password = user.Password,
                Telephone = user.Telephone,
            };
            return _userRepository.AddAsync(entity);
        }

        public Task<bool> UpdateAsync(Guid id, CreateUserDto user)
        {
            var existedUser = _userRepository.GetByIdAsync(id);
            if (existedUser == null)
            {
                return Task.Run(() => true);
            }

            Domain.User entity = existedUser.Result;
            entity.Name = user.Name;
            entity.Email = user.Email;
            entity.Password = user.Password;
            entity.Telephone = user.Telephone;

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
