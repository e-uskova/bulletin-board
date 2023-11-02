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

        public async Task<IEnumerable<CategoryDto>?> GetAllAsync(CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAll().ToListAsync(cancellationToken);
            if (categories == null)
            {
                return null;
            }
            var result = (from category in categories
                          select Mapper.ToCategoryDto(category)).AsEnumerable();
            return result;
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            return Mapper.ToCategoryDto(category);
        }

        public async Task<IEnumerable<CategoryDto>?> GetWithChildrenByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var ids = new List<Guid>() { id };
            ids.AddRange(await GetAllChildrenIdAsync(id, cancellationToken));

            var categories = await _categoryRepository.GetRangeByIDAsync(ids, cancellationToken);

            if (categories == null)
            {
                return null;
            }
            var result = (from category in categories
                          select Mapper.ToCategoryDto(category)).AsEnumerable();

            return result;
        }

        public async Task<List<Guid>> GetAllChildrenIdAsync(Guid parentId, CancellationToken cancellationToken)
        {
            var children = await GetChildrenIdAsync(parentId, cancellationToken);
            if (children.Count == 0)
            {
                return new List<Guid>();
            }
            var result = new List<Guid>(children);

            foreach (var child in children)
            {
                result.AddRange(await GetAllChildrenIdAsync(child, cancellationToken));
            }

            return result;
        }

        public async Task<List<Guid>> GetChildrenIdAsync(Guid parentGuid, CancellationToken cancellationToken)
        {
            var ids = await _categoryRepository.GetAll().Where(c => c.ParentCategory != null && c.ParentCategory.Id == parentGuid).Select(c => c.Id).ToListAsync(cancellationToken);
            return ids;
        }

        public async Task<CategoryDto?> GetFirstWhere(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetFirstWhere(predicate, cancellationToken);
            return Mapper.ToCategoryDto(category);
        }

        public async Task<IEnumerable<CategoryDto>?> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetRangeByIDAsync(ids, cancellationToken);
            if (categories == null)
            {
                return null;
            }
            var result = (from category in categories
                          select Mapper.ToCategoryDto(category)).AsEnumerable();
            return result;
        }

        public async Task<IEnumerable<CategoryDto>?> GetWhere(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetWhere(predicate).ToListAsync(cancellationToken);
            if (categories == null)
            {
                return null;
            }
            var result = (from category in categories
                          select Mapper.ToCategoryDto(category)).AsEnumerable();
            return result;
        }

        public async Task<Guid> AddAsync(CreateCategoryDto category, CancellationToken cancellationToken)
        {
            Category entity = new()
            {
                Name = category.Name,
            };
            if (category.ParentCategoryId != null) 
            {
                entity.ParentCategory = await _categoryRepository.GetByIdAsync((Guid)category.ParentCategoryId, cancellationToken);
            }
            return await _categoryRepository.AddAsync(entity, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Guid id, EditCategoryDto category, CancellationToken cancellationToken)
        {
            var entity = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (category.Name != null)
            {   
                entity.Name = category.Name;
            }
            if (category.ParentCategoryId != null)
            {
                entity.ParentCategory = await _categoryRepository.GetByIdAsync((Guid)category.ParentCategoryId, cancellationToken);                
            }

            await _categoryRepository.UpdateAsync(entity, cancellationToken);
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedCategory = await _categoryRepository.GetByIdAsync(id, cancellationToken);

            await _categoryRepository.DeleteAsync(existedCategory, cancellationToken);
            return false;
        }
    }
}
