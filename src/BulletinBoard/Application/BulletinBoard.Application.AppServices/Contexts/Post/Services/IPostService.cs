﻿using BulletinBoard.Contracts.Post;
using BulletinBoard.Contracts.User;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Post.Services
{
    /// <summary>
    /// Сервис работы с объявлениями.
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// Получение всех элементов. 
        /// </summary>
        /// <returns>Коллекция элементов типа <see cref="PostDto"/></returns>
        Task<IEnumerable<PostDto>?> GetAllAsync(CancellationToken cancellationToken, int pageSize, int pageIndex);

        /// <summary>
        /// Получение элемента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента.</param>
        /// <returns>Элемент типа <see cref="PostDto"/></returns>
        Task<PostDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение элементов по списку идентификаторов.
        /// </summary>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Коллекция элементов типа <see cref="PostDto"/></returns>
        Task<IEnumerable<PostDto>?> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Получение первого элемента из удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Элемент типа <see cref="PostDto"/></returns>
        Task<PostDto?> GetFirstWhere(Expression<Func<Domain.Post, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Получение всех элементов, удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Коллекция элементов типа <see cref="PostDto"/></returns>
        Task<IEnumerable<PostDto>?> GetWhere(Expression<Func<Domain.Post, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление элемента.
        /// </summary>
        /// <param name="entity">Объявление</param>
        /// <param name="curUser">Текущий пользователь</param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreatePostDto entity, UserDto curUser, CancellationToken cancellationToken);

        /// <summary>
        /// Изменение элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid id, EditPostDto entity, CancellationToken cancellationToken);

        public Task<bool> CloseAsync(Guid id, CancellationToken cancellationToken);

        public Task<bool> ReOpenAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
