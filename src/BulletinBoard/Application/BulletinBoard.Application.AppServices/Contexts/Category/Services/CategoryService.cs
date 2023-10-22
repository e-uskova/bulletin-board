using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Contracts.Categories;
using System.Linq.Expressions;

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

        public Task<CategoryDto?> GetByIdAsync(Guid id)
        {
            return _categoryRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<CategoryDto?>> GetWithChildrenByIdAsync(Guid id)
        {
            return _categoryRepository.GetWithChildrenByIdAsync(id);
        }

        public Task<CategoryDto> GetFirstWhere(Expression<Func<Domain.Category, bool>> predicate)
        {
            return _categoryRepository.GetFirstWhere(predicate);
        }

        public Task<IEnumerable<CategoryDto>> GetRangeByIDAsync(List<Guid> ids)
        {
            return _categoryRepository.GetRangeByIDAsync(ids);
        }

        public Task<IEnumerable<CategoryDto>> GetWhere(Expression<Func<Domain.Category, bool>> predicate)
        {
            return _categoryRepository.GetWhere(predicate);
        }

        public Task<Guid> AddAsync(CreateCategoryDto category)
        {
            return _categoryRepository.AddAsync(category);
        }

        public Task<bool> UpdateAsync(Guid id, CreateCategoryDto category)
        {
            return _categoryRepository.UpdateAsync(id, category);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _categoryRepository.DeleteAsync(id);
        }

    }
}
