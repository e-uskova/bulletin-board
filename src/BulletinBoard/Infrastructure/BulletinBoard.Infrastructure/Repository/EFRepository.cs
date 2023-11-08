using BulletinBoard.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.Repository
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _dataContext;
        private readonly DbSet<T> _dbSet;

        public EFRepository(DbContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (_dbSet.Any())
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            }
            return null;
        }

        public async Task<IEnumerable<T>> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }
            var entities = await _dbSet.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
            return entities;
        }

        public async Task<T?> GetFirstWhere(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            var entity = await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
            return entity;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            return _dbSet.Where(predicate);
        }

        public async Task<Guid> AddAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _dbSet.AddAsync(entity, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbSet.Update(entity);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbSet.Remove(entity);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
