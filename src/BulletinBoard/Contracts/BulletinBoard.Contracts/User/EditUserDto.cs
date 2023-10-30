using BulletinBoard.Contracts.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.User
{
    /// <summary>
    /// Изменение пользователя.
    /// </summary>
    public class EditUserDto
    {
        /// <summary>
        /// Имя.
        /// </summary>
        [StringLength(50, MinimumLength = 2)]
        public string? Name { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Password(20, 3)]
        public string? Password { get; set; }

        /// <summary>
        /// Подтверждение пароля.
        /// </summary>
        [Compare("Password")]
        public string? PasswordConfirm { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        [Phone]
        public string? Telephone { get; set; }
    }
}
