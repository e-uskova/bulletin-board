using BulletinBoard.Contracts.Post;
using BulletinBoard.Contracts.Users;
using System.Linq.Expressions;

namespace BulletinBoard.Application.AppServices.Contexts.Post.Repositories
{
    /// <summary>
    /// Репозиторий для работы с объявлениями.
    /// </summary>
    public interface IPostRepository
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
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreatePostDto entity, UserDto curUser, CancellationToken cancellationToken);

        /// <summary>
        /// Изменение элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid id, EditPostDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Деактивация объявления.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> CloseAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Активация объявления.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ReOpenAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Отметка об изменении.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ModifyAsync(Guid id, CancellationToken cancellationToken);
    }
}
