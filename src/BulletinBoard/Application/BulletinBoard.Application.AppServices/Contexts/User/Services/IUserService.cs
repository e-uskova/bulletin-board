using BulletinBoard.Contracts.Users;

namespace BulletinBoard.Application.AppServices.Contexts.User.Services
{
    /// <summary>
    /// Сервис работы с пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получение пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель пользователя <see cref="UserDto"/></returns>
        Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создание пользователя по модели.
        /// </summary>
        /// <param name="model">Модель пользователя.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданой сущности.</returns>
        Task<Guid> CreateAsync(CreateUserDto model, CancellationToken cancellationToken);
    }
}
