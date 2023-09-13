﻿using BulletinBoard.Contracts.Base;
using BulletinBoard.Contracts.Post;

namespace BulletinBoard.Contracts.Users
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserDto : BaseDto
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
        public IReadOnlyCollection<PostDto> Posts { get; set; }
    }
}