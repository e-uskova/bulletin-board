using BulletinBoard.Contracts.Base;
using BulletinBoard.Contracts.Post;
using System.Security.Claims;

namespace BulletinBoard.Contracts.Users
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserDto : BaseDto
    {
        public bool IsAuthenticated {  get; set; }

        public string Scheme { get; set; }

        public List<object> Claims { get; set; } = new List<object>();


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
        public IReadOnlyCollection<PostDto> Posts { get; set; }
    }
}
