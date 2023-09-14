﻿using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.User.Services
{
    /// <summary>
    /// Сервис работы с пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получение всех элементов. 
        /// </summary>
        /// <returns>Коллекция элементов типа <see cref="Domain.Users.User"/></returns>
        Task<IEnumerable<Domain.Users.User>> GetAllAsync();

        /// <summary>
        /// Получение элемента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента.</param>
        /// <returns>Элемент типа <see cref="Domain.Users.User"/></returns>
        Task<Domain.Users.User> GetByIdAsync(Guid id);

        /// <summary>
        /// Получение элементов по списку идентоификаторов.
        /// </summary>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Коллекция элементов типа <see cref="T"/></returns>
        Task<IEnumerable<Domain.Users.User>> GetRangeByIDAsync(List<Guid> ids);

        /// <summary>
        /// Получение первого элемента из удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Элемент типа <see cref="T"/></returns>
        Task<Domain.Users.User> GetFirstWhere(Expression<Func<Domain.Users.User, bool>> predicate);

        /// <summary>
        /// Получение всех элементов, удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Коллекция элементов типа <see cref="T"/></returns>
        Task<IEnumerable<Domain.Users.User>> GetWhere(Expression<Func<Domain.Users.User, bool>> predicate);

        /// <summary>
        /// Добавление элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(Domain.Users.User entity);

        /// <summary>
        /// Изменение элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(Domain.Users.User entity);

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(Domain.Users.User entity);
    }
}
