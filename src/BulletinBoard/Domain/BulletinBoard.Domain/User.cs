namespace BulletinBoard.Domain
{
    /// <summary>
    /// Сущность пользователя.
    /// </summary>
    public class User : BaseEntity
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
        
        /// <summary>
        /// Объявления.
        /// </summary>
        public IReadOnlyCollection<Post> Posts { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Telephone { get; set; }
    }
}
