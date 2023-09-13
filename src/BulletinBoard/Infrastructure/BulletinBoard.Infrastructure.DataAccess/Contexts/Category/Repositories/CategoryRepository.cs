using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Contracts.Categories;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Category.Repositories
{
    /// <inheritdoc/>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<Domain.Categories.Category> _categories = new();

        /// <inheritdoc/>
        public Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.Run(() => new CategoryDto
            {
                CategoryName = "Books",
                //ParentCategoryId = Guid.NewGuid(),
            }, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> CreateAsync(Domain.Categories.Category model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            _categories.Add(model);
            return Task.Run(() => model.Id);
        }
    }
}
