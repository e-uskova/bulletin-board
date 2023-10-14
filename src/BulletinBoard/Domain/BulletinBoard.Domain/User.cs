namespace BulletinBoard.Domain
{
    /// <summary>
    /// Сущность пользователя.
    /// </summary>
    public class User : BaseEntity
    {
        /*/// <summary>
        /// Флаг, что пользователь аутентифицирован.
        /// </summary>
        public bool IsAuthenticated { get; set; }

        public string Scheme { get; set; }

        public List<object> Claims { get; set; } = new List<object>();*/

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
