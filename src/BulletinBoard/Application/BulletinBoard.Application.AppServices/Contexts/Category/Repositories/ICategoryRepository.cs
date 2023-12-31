﻿using BulletinBoard.Contracts.Categories;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Category.Repositories
{
    /// <summary>
    /// Репозиторий для работы с категориями.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Получение всех элементов. 
        /// </summary>
        /// <returns>Коллекция элементов типа <see cref="CategoryDto"/></returns>
        Task<IEnumerable<CategoryDto>?> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получение элемента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента.</param>
        /// <returns>Элемент типа <see cref="CategoryDto"/></returns>
        Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<CategoryDto>?> GetWithChildrenByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение идентификаторов всех дочерних категорий.
        /// </summary>
        /// <param name="parentId">Идентификатор родительской категории.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Guid>> GetAllChildrenIdAsync(Guid parentId, CancellationToken cancellationToken);

        /// <summary>
        /// Получение идентификаторов дочерних категорий первого порядка.
        /// </summary>
        /// <param name="parentGuid">Идентификатор родительской категории.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Guid>> GetChildrenIdAsync(Guid parentGuid, CancellationToken cancellationToken);

        /// <summary>
        /// Получение элементов по списку идентификаторов.
        /// </summary>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Коллекция элементов типа <see cref="CategoryDto"/></returns>
        Task<IEnumerable<CategoryDto>?> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Получение первого элемента из удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Элемент типа <see cref="CategoryDto"/></returns>
        Task<CategoryDto?> GetFirstWhere(Expression<Func<Domain.Category, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Получение всех элементов, удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Коллекция элементов типа <see cref="CategoryDto"/></returns>
        Task<IEnumerable<CategoryDto>?> GetWhere(Expression<Func<Domain.Category, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateCategoryDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Изменение элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid id, EditCategoryDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
