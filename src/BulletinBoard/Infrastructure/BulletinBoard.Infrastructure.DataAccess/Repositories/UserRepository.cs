using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.User;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Domain;
using BulletinBoard.Infrastructure.Repository;
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
            var users = _userRepository.GetAll().ToListAsync().Result;
            if (users == null)
            {
                return Task.FromResult<IEnumerable<UserDto>?>(null);
            }
            var result = (from user in users
                          select Mapper.ToUserDto(user)).AsEnumerable();
            return Task.Run(() => result);
        }

        public Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetByIdAsync(id, cancellationToken).Result;
            if (user == null)
            {
                return Task.FromResult<UserDto?>(null);
            }
            return Task.Run(() => Mapper.ToUserDto(user));
        }

        /*public Task<User?> GetCurrentUserAsync()
        {
            var curUser = _userRepository.GetAll().Where(u => u.Email == ClaimTypes.Email?.Value).FirstOrDefault();
            return Task.Run(() => curUser);
        }*/

        public Task<UserDto> GetFirstWhere(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetFirstWhere(predicate, cancellationToken).Result;
            if (user == null)
            {
                return Task.FromResult<UserDto?>(null);
            }
            return Task.Run(() => Mapper.ToUserDto(user));
        }

        public Task<IEnumerable<UserDto>> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            var users = _userRepository.GetRangeByIDAsync(ids, cancellationToken).Result;
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

        public Task<Guid> AddAsync(CreateUserDto user, CancellationToken cancellationToken)
        {
            Domain.User entity = new()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
            if (user.Telephone != null)
            {
                entity.Telephone = user.Telephone;
            }
            return _userRepository.AddAsync(entity, cancellationToken);
        }

        public Task<bool> UpdateAsync(Guid id, EditUserDto user, CancellationToken cancellationToken)
        {
            var existedUser = _userRepository.GetByIdAsync(id, cancellationToken).Result;
            if (existedUser == null)
            {
                return Task.Run(() => true);
            }

            Domain.User entity = existedUser;
            if (user.Name != null)
            {
                entity.Name = user.Name;
            }
            if (user.Password != null)
            {
                entity.Password = user.Password;
            }
            if (user.Telephone != null)
            {
                entity.Telephone = user.Telephone;
            }

            _userRepository.UpdateAsync(entity, cancellationToken);
            return Task.Run(() => false);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedUser = _userRepository.GetByIdAsync(id, cancellationToken);
            if (existedUser == null)
            {
                return Task.Run(() => true);
            }

            _userRepository.DeleteAsync(existedUser.Result, cancellationToken);
            return Task.Run(() => false);
        }
    }
}
