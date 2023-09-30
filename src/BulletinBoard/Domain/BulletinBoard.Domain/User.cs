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
        public string UserName { get; set; }
        
        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string UserEmail { get; set; }
        
        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Объявления.
        /// </summary>
        public IReadOnlyCollection<Post> Posts { get; set; }
    }
}
