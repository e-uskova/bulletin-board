﻿using BulletinBoard.Contracts.User;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.User.Repositories
{
    /// <summary>
    /// Репозиторий для работы с пользователями.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получение всех элементов. 
        /// </summary>
        /// <returns>Коллекция элементов типа <see cref="UserDto"/></returns>
        Task<IEnumerable<UserDto>?> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получение элемента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента.</param>
        /// <returns>Элемент типа <see cref="UserDto"/></returns>
        Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение элементов по списку идентификаторов.
        /// </summary>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Коллекция элементов типа <see cref="UserDto"/></returns>
        Task<IEnumerable<UserDto>?> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Получение первого элемента из удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Элемент типа <see cref="UserDto"/></returns>
        Task<UserDto?> GetFirstWhere(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Получение всех элементов, удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Коллекция элементов типа <see cref="UserDto"/></returns>
        Task<IEnumerable<UserDto>?> GetWhere(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateUserDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Изменение элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid id, EditUserDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
