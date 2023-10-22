using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.Categories;
using BulletinBoard.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulletinBoard.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryRepository(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categorys = _categoryRepository.GetAllAsync().ToListAsync().Result;
            if (categorys == null)
            {
                return Task.FromResult<IEnumerable<CategoryDto>?>(null);
            }
            var result = (from category in categorys
                          select Mapper.ToCategoryDto(category)).AsEnumerable();
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

        public Task<IEnumerable<CategoryDto>> GetWithChildrenByIdAsync(Guid id)
        {
            var ids = new List<Guid>() { id };
            ids.AddRange(GetAllChildrenId(id));

            var categories = _categoryRepository.GetRangeByIDAsync(ids).Result;
            var result = new List<CategoryDto>();
            foreach (var category in categories)
            {
                result.Add(Mapper.ToCategoryDto(category));
            }
            return Task.Run(() => result.AsEnumerable());
        }

        private List<Guid> GetAllChildrenId(Guid parentId)
        {
            var children = GetChildrenId(parentId);
            if (children.Count == 0)
            {
                return new List<Guid>();
            }
            var result = new List<Guid>(children);

            foreach (var child in children)
            {
                result.AddRange(GetAllChildrenId(child));
            }

            return result;
        }

        private List<Guid> GetChildrenId(Guid parentGuid)
        {
            var ids = _categoryRepository.GetAllAsync().Where(c => c.ParentCategory != null && c.ParentCategory.Id == parentGuid).Select(c => c.Id).ToList();
            return ids;
        }

        public Task<CategoryDto> GetFirstWhere(Expression<Func<Category, bool>> predicate)
        {
            var category = _categoryRepository.GetFirstWhere(predicate).Result;
            if (category == null)
            {
                return Task.FromResult<CategoryDto?>(null);
            }
            return Task.Run(() => Mapper.ToCategoryDto(category));
        }

        public Task<IEnumerable<CategoryDto>> GetRangeByIDAsync(List<Guid> ids)
        {
            var categorys = _categoryRepository.GetRangeByIDAsync(ids).Result;
            IEnumerable<CategoryDto> result = new List<CategoryDto>();
            foreach (var category in categorys)
            {
                result = result.Append(Mapper.ToCategoryDto(category));
            }
            return Task.Run(() => result);
        }

        public Task<IEnumerable<CategoryDto>> GetWhere(Expression<Func<Category, bool>> predicate)
        {
            var categorys = _categoryRepository.GetWhere(predicate).AsEnumerable();
            IEnumerable<CategoryDto> result = new List<CategoryDto>();
            foreach (var category in categorys)
            {
                result = result.Append(Mapper.ToCategoryDto(category));
            }
            return Task.Run(() => result);
        }

        public Task<Guid> AddAsync(CreateCategoryDto category)
        {
            Category entity = new()
            {
                Id = Guid.NewGuid(),
                Name = category.Name,
            };
            if (category.ParentCategoryId != null) 
            {
                entity.ParentCategory = _categoryRepository.GetByIdAsync((Guid)category.ParentCategoryId).Result;
            }
            return _categoryRepository.AddAsync(entity);
        }

        public Task<bool> UpdateAsync(Guid id, CreateCategoryDto category)
        {
            var existedCategory = _categoryRepository.GetByIdAsync(id);
            if (existedCategory == null)
            {
                return Task.Run(() => true);
            }

            Category entity = existedCategory.Result;
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
