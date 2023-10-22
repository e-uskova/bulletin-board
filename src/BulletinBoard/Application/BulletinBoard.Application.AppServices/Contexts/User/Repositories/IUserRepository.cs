using BulletinBoard.Contracts.Users;
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
        Task<IEnumerable<UserDto>> GetAllAsync();

        /// <summary>
        /// Получение элемента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента.</param>
        /// <returns>Элемент типа <see cref="UserDto"/></returns>
        Task<UserDto?/*Domain.User*/> GetByIdAsync(Guid id);

        /*/// <summary>
        /// Получение текущего пользователя.
        /// </summary>
        /// <returns>Сущность пользователя.</returns>
        public Task<Domain.User?> GetCurrentUserAsync();*/

        /// <summary>
        /// Получение элементов по списку идентификаторов.
        /// </summary>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Коллекция элементов типа <see cref="UserDto"/></returns>
        Task<IEnumerable<UserDto>> GetRangeByIDAsync(List<Guid> ids);

        /// <summary>
        /// Получение первого элемента из удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Элемент типа <see cref="UserDto"/></returns>
        Task<UserDto> GetFirstWhere(Expression<Func<Domain.User, bool>> predicate);

        /// <summary>
        /// Получение всех элементов, удовлетворяющих условию.
        /// </summary>
        /// <param name="predicate">Условие отбора.</param>
        /// <returns>Коллекция элементов типа <see cref="UserDto"/></returns>
        Task<IEnumerable<UserDto>> GetWhere(Expression<Func<Domain.User, bool>> predicate);

        /// <summary>
        /// Добавление элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateUserDto entity);

        /// <summary>
        /// Изменение элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid id, CreateUserDto entity);

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
