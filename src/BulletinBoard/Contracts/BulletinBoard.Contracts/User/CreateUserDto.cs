namespace BulletinBoard.Contracts.Users
{
    /// <summary>
    /// Создание пользователя.
    /// </summary>
    public class CreateUserDto
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }
    }
}
