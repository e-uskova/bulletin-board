using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Contracts.Categories;

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

        /// <inheritdoc/>
        public Task<IEnumerable<CategoryDto>?> GetAllAsync(CancellationToken cancellationToken)
        {
            return _categoryRepository.GetAllAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<CategoryDto>?> GetWithChildrenByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetWithChildrenByIdAsync(id, cancellationToken);
        }        

        /// <inheritdoc/>
        public async Task<Guid> AddAsync(CreateCategoryDto category, CancellationToken cancellationToken)
        {
            if (category.ParentCategoryId != null)
            {
                var parentCategory = await _categoryRepository.GetByIdAsync((Guid)category.ParentCategoryId, cancellationToken);
                if (parentCategory == null)
                {
                    return Guid.Empty;
                }
            }

            return await _categoryRepository.AddAsync(category, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateAsync(Guid id, EditCategoryDto category, CancellationToken cancellationToken)
        {
            var existedCategory = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (existedCategory == null)
            {
                return true;
            }

            if (category.ParentCategoryId != null)
            {
                if (category.ParentCategoryId == id)
                {
                    return true;
                }
                var parentCategory = await _categoryRepository.GetByIdAsync((Guid)category.ParentCategoryId, cancellationToken);
                if (parentCategory == null)
                {
                    return true;
                }
                var childrenIds = await _categoryRepository.GetChildrenIdAsync(id, cancellationToken);
                if (childrenIds.Count > 0 && childrenIds.Contains((Guid)category.ParentCategoryId)) // новая родительская категория является дочерней
                {
                    return true;
                }
            }

            return await _categoryRepository.UpdateAsync(id, category, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedCategory = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (existedCategory == null)
            {
                return true;
            }
            var childrenIds = await _categoryRepository.GetChildrenIdAsync(id, cancellationToken);
            foreach (var childId in childrenIds) // изменение родительской категории дочерних записей на родительскую удаляемой
            {
                await _categoryRepository.UpdateAsync(childId, new EditCategoryDto() { ParentCategoryId = existedCategory.ParentCategoryId }, cancellationToken);
            }

            return await _categoryRepository.DeleteAsync(id, cancellationToken);
        }

    }
}
