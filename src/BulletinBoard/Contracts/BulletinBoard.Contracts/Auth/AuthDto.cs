using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Auth
{
    /// <summary>
    /// Данные для аутентификации.
    /// </summary>
    public class AuthDto
    {
        /// <summary>
        /// Логин (email).
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
