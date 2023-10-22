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
        Task<IEnumerable<CategoryDto>> GetAllAsync();

        /// <summary>
        /// Получение элемента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента.</param>
        /// <returns>Элемент типа <see cref="CategoryDto"/></returns>
        Task<CategoryDto?/*Domain.Category*/> GetByIdAsync(Guid id);

        Task<IEnumerable<CategoryDto?>> GetWithChildrenByIdAsync(Guid id);

        /// <summary>
        /// Получение элементов по списку идентификаторов.
        /// </summary>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Коллекция элементов типа <see cref="CategoryDto"/></returns>
        Task<IEnumerable<CategoryDto>> GetRangeByIDAsync(List<Guid> ids);

        /// <summary>
        /// Получение первого элемента из удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Элемент типа <see cref="CategoryDto"/></returns>
        Task<CategoryDto> GetFirstWhere(Expression<Func<Domain.Category, bool>> predicate);

        /// <summary>
        /// Получение всех элементов, удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Коллекция элементов типа <see cref="CategoryDto"/></returns>
        Task<IEnumerable<CategoryDto>> GetWhere(Expression<Func<Domain.Category, bool>> predicate);

        /// <summary>
        /// Добавление элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateCategoryDto entity);

        /// <summary>
        /// Изменение элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid id, CreateCategoryDto entity);

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
