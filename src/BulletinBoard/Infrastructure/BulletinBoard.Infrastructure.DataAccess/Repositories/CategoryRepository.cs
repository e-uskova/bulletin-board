using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.Categories;
using BulletinBoard.Domain;
using BulletinBoard.Infrastructure.Repository;
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
            var categorys = _categoryRepository.GetAll().ToListAsync().Result;
            if (categorys == null)
            {
                return Task.FromResult<IEnumerable<CategoryDto>?>(null);
            }
            var result = (from category in categorys
                          select Mapper.ToCategoryDto(category)).AsEnumerable();
            return Task.Run(() => result);
        }

        public Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var category = _categoryRepository.GetByIdAsync(id, cancellationToken).Result;
            if (category == null)
            {
                return Task.FromResult<CategoryDto?>(null);
            }
            return Task.Run(() => Mapper.ToCategoryDto(category));
        }

        public Task<IEnumerable<CategoryDto>> GetWithChildrenByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var ids = new List<Guid>() { id };
            ids.AddRange(GetAllChildrenId(id));

            var categories = _categoryRepository.GetRangeByIDAsync(ids, cancellationToken).Result;
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
            var ids = _categoryRepository.GetAll().Where(c => c.ParentCategory != null && c.ParentCategory.Id == parentGuid).Select(c => c.Id).ToList();
            return ids;
        }

        public Task<CategoryDto> GetFirstWhere(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
        {
            var category = _categoryRepository.GetFirstWhere(predicate, cancellationToken).Result;
            if (category == null)
            {
                return Task.FromResult<CategoryDto?>(null);
            }
            return Task.Run(() => Mapper.ToCategoryDto(category));
        }

        public Task<IEnumerable<CategoryDto>> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            var categorys = _categoryRepository.GetRangeByIDAsync(ids, cancellationToken).Result;
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

        public Task<Guid> AddAsync(CreateCategoryDto category, CancellationToken cancellationToken)
        {
            Category entity = new()
            {
                Id = Guid.NewGuid(),
                Name = category.Name,
            };
            if (category.ParentCategoryId != null) 
            {
                entity.ParentCategory = _categoryRepository.GetByIdAsync((Guid)category.ParentCategoryId, cancellationToken).Result;
            }
            return _categoryRepository.AddAsync(entity, cancellationToken);
        }

        public Task<bool> UpdateAsync(Guid id, EditCategoryDto category, CancellationToken cancellationToken)
        {
            var existedCategory = _categoryRepository.GetByIdAsync(id, cancellationToken).Result;
            if (existedCategory == null)
            {
                return Task.Run(() => true);
            }

            Category entity = existedCategory;
            if (category.Name != null)
            {
                entity.Name = category.Name;
            }
            if (category.ParentCategoryId != null)
            {
                var parentCategory = _categoryRepository.GetByIdAsync((Guid)category.ParentCategoryId, cancellationToken).Result;
                if (parentCategory != null)
                {
                    entity.ParentCategory = parentCategory;
                }
            }

            _categoryRepository.UpdateAsync(entity, cancellationToken);
            return Task.Run(() => false);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedCategory = _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (existedCategory == null)
            {
                return Task.Run(() => true);
            }

            _categoryRepository.DeleteAsync(existedCategory.Result, cancellationToken);
            return Task.Run(() => false);
        }
    }
}
