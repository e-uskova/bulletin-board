using BulletinBoard.Contracts.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        [PasswordAttribute(20, 3)]
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля.
        /// </summary>
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        [Phone]
        public string Telephone { get; set; }
    }
}
