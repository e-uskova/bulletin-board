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
        public string UserName { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }
    }
}
