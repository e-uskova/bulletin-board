using BulletinBoard.Contracts.Users;
using System.Linq.Expressions;

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
        /// <returns>Коллекция элементов типа <see cref="UserDto"/></returns>
        Task<IEnumerable<UserDto>> GetAllAsync();

        /// <summary>
        /// Получение элемента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента.</param>
        /// <returns>Элемент типа <see cref="UserDto"/></returns>
        Task<UserDto> GetByIdAsync(Guid id);

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
        Task UpdateAsync(UserDto entity);

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(UserDto entity);
    }
}