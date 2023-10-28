using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Contracts.Categories;
using System.Linq.Expressions;
using System.Threading;

namespace BulletinBoard.Application.AppServices.Contexts.Category.Services
{
    /// <inheritdoc/>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="CategoryService"/>
        /// </summary>
        /// <param name="categoryRepository">Репозиторий для работы с пользователями.</param>
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return _categoryRepository.GetAllAsync();
        }

        public Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IEnumerable<CategoryDto?>> GetWithChildrenByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetWithChildrenByIdAsync(id, cancellationToken);
        }

        public Task<CategoryDto> GetFirstWhere(Expression<Func<Domain.Category, bool>> predicate, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetFirstWhere(predicate, cancellationToken);
        }

        public Task<IEnumerable<CategoryDto>> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetRangeByIDAsync(ids, cancellationToken);
        }

        public Task<IEnumerable<CategoryDto>> GetWhere(Expression<Func<Domain.Category, bool>> predicate)
        {
            return _categoryRepository.GetWhere(predicate);
        }

        public Task<Guid> AddAsync(CreateCategoryDto category, CancellationToken cancellationToken)
        {
            return _categoryRepository.AddAsync(category, cancellationToken);
        }

        public Task<bool> UpdateAsync(Guid id, CreateCategoryDto category, CancellationToken cancellationToken)
        {
            return _categoryRepository.UpdateAsync(id, category, cancellationToken);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _categoryRepository.DeleteAsync(id, cancellationToken);
        }

    }
}
