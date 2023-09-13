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
        /// <param name="categoryRepository">Репозиторий для работы с категориями</param>
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <inheritdoc/>
        public Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(CreateCategoryDto model, CancellationToken cancellationToken)
        {
            var category = new Domain.Categories.Category()
            {
                CategoryName = model.CategoryName,
                //ParentCategoryId = Guid.NewGuid(), //model.ParentCategoryName, // GetIdByName()
                //SubCategoriesId =  // GetSubCategoriesId()
            };
            return _categoryRepository.CreateAsync(category, cancellationToken);
        }

    }
}
