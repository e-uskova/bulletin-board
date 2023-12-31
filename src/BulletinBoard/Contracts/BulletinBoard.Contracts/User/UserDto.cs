﻿using BulletinBoard.Contracts.Base;
using BulletinBoard.Contracts.Post;

namespace BulletinBoard.Contracts.User
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserDto : BaseDto
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string Email { get; set; }

        /*/// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }*/

        /// <summary>
        /// Объявления.
        /// </summary>
        public IReadOnlyCollection<PostDto> Posts { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Telephone { get; set; }
    }
}
