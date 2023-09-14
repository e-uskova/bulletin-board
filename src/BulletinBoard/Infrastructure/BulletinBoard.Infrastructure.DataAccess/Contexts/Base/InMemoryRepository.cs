using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using BulletinBoard.Domain.Base;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Base
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected List<T> Data {  get; set; }

        public InMemoryRepository(IEnumerable<T> data) 
        {
            Data = data.ToList();    
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data.AsEnumerable());
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<T>> GetRangeByIDAsync(List<Guid> ids)
        {
            return Task.FromResult(Data.Where(x => ids.Contains(x.Id)).AsEnumerable());
        }

        public Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate)
        {
            return Task.FromResult(Data.Where(predicate.Compile()).FirstOrDefault());
        }

        public Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return Task.FromResult(Data.Where(predicate.Compile()).AsEnumerable());
        }

        public Task AddAsync(T entity)
        {
            Data.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            var existed = Data.FirstOrDefault(x => x.Id == entity.Id);
            if (existed != null)
            {
                Data[Data.IndexOf(existed)] = existed;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            Data.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
