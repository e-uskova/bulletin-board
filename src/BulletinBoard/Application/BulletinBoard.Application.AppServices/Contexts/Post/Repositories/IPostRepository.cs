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
        Task<IEnumerable<PostDto>> GetAllAsync();

        /// <summary>
        /// Получение элемента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента.</param>
        /// <returns>Элемент типа <see cref="PostDto"/></returns>
        Task<PostDto?/*Domain.Post*/> GetByIdAsync(Guid id);

        /// <summary>
        /// Получение элементов по списку идентификаторов.
        /// </summary>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Коллекция элементов типа <see cref="PostDto"/></returns>
        Task<IEnumerable<PostDto>> GetRangeByIDAsync(List<Guid> ids);

        /// <summary>
        /// Получение первого элемента из удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Элемент типа <see cref="PostDto"/></returns>
        Task<PostDto> GetFirstWhere(Expression<Func<Domain.Post, bool>> predicate);

        /// <summary>
        /// Получение всех элементов, удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Коллекция элементов типа <see cref="PostDto"/></returns>
        Task<IEnumerable<PostDto>> GetWhere(Expression<Func<Domain.Post, bool>> predicate);

        /// <summary>
        /// Добавление элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreatePostDto entity, UserDto curUser);

        /// <summary>
        /// Изменение элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid id, CreatePostDto entity);

        public Task CloseAsync(Guid id);

        public Task ReOpenAsync(Guid id);

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
