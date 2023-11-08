using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.User;
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

        public async Task<IEnumerable<UserDto>?> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll().ToListAsync(cancellationToken);
            if (users == null)
            {
                return null;
            }
            var result = (from user in users
                          select Mapper.ToUserDto(user)).AsEnumerable();
            return result;
        }

        public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            return Mapper.ToUserDto(user);
        }

        public async Task<UserDto?> GetFirstWhere(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetFirstWhere(predicate, cancellationToken);
            return Mapper.ToUserDto(user);
        }

        public async Task<IEnumerable<UserDto>?> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetRangeByIDAsync(ids, cancellationToken);
            if (users == null)
            {
                return null;
            }
            var result = (from user in users
                          select Mapper.ToUserDto(user)).AsEnumerable();
            return result;
        }

        public async Task<IEnumerable<UserDto>?> GetWhere(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetWhere(predicate).ToListAsync(cancellationToken);
            if (users == null)
            {
                return null;
            }
            var result = (from user in users
                          select Mapper.ToUserDto(user)).AsEnumerable();
            return result;
        }

        public Task<Guid> AddAsync(CreateUserDto user, CancellationToken cancellationToken)
        {
            User entity = new()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Telephone = user.Telephone
            };
            return _userRepository.AddAsync(entity, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Guid id, EditUserDto user, CancellationToken cancellationToken)
        {
            var existedUser = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (existedUser == null)
            {
                return true;
            }

            User entity = existedUser;
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

            await _userRepository.UpdateAsync(entity, cancellationToken);
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedUser = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (existedUser == null)
            {
                return true;
            }

            await _userRepository.DeleteAsync(existedUser, cancellationToken);
            return false;
        }
    }
}
