using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.Categories;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Category.Services
{
    /// <inheritdoc/>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="UserService"/>
        /// </summary>
        /// <param name="categoryRepository">Репозиторий для работы с пользователями.</param>
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = _categoryRepository.GetAllAsync().Result;
            IEnumerable<CategoryDto> result = new List<CategoryDto>();
            foreach (var category in categories)
            {
                result = result.Append(Mapper.ToCategoryDto(category));
            }
            return Task.Run(() => result);
        }

        public Task<CategoryDto?> GetByIdAsync(Guid id)
        {
            var category = _categoryRepository.GetByIdAsync(id).Result;
            if (category == null)
            {
                return Task.FromResult<CategoryDto?>(null);
            }
            return Task.Run(() => Mapper.ToCategoryDto(category));
        }

        public Task<CategoryDto> GetFirstWhere(Expression<Func<Domain.Category, bool>> predicate)
        {
            var category = _categoryRepository.GetFirstWhere(predicate).Result;
            return Task.Run(() => Mapper.ToCategoryDto(category));
        }

        public Task<IEnumerable<CategoryDto>> GetRangeByIDAsync(List<Guid> ids)
        {
            var categories = _categoryRepository.GetRangeByIDAsync(ids).Result;
            IEnumerable<CategoryDto> result = new List<CategoryDto>();
            foreach (var category in categories)
            {
                result = result.Append(Mapper.ToCategoryDto(category));
            }
            return Task.Run(() => result);
        }

        public Task<IEnumerable<CategoryDto>> GetWhere(Expression<Func<Domain.Category, bool>> predicate)
        {
            var categories = _categoryRepository.GetWhere(predicate).Result;
            IEnumerable<CategoryDto> result = new List<CategoryDto>();
            foreach (var category in categories)
            {
                result = result.Append(Mapper.ToCategoryDto(category));
            }
            return Task.Run(() => result);
        }

        public Task<Guid> AddAsync(CreateCategoryDto category)
        {
            Domain.Category entity = new()
            {
                Id = Guid.NewGuid(),
                Name = category.Name,
                ParentCategory = _categoryRepository.GetByIdAsync((Guid)category.ParentCategoryId).Result,
            };
            return _categoryRepository.AddAsync(entity);
        }

        public Task<bool> UpdateAsync(Guid id, CreateCategoryDto category)
        {
            var existedCategory = _categoryRepository.GetByIdAsync(id);
            if (existedCategory == null)
            {
                return Task.Run(() => true);
            }

            Domain.Category entity = existedCategory.Result;
            entity.Name = category.Name;            

            if (category.ParentCategoryId != null)
            {
                entity.ParentCategory = _categoryRepository.GetByIdAsync((Guid)category.ParentCategoryId).Result;
            }

            _categoryRepository.UpdateAsync(entity);
            return Task.Run(() => false);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var existedCategory = _categoryRepository.GetByIdAsync(id);
            if (existedCategory == null)
            {
                return Task.Run(() => true);
            }

            _categoryRepository.DeleteAsync(existedCategory.Result);
            return Task.Run(() => false);
        }

    }
}
