﻿using BulletinBoard.Contracts.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.User
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
        [StringLength(64, MinimumLength = 2)]
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
        [Password(20, 6)]
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
        public string? Telephone { get; set; }
    }
}
