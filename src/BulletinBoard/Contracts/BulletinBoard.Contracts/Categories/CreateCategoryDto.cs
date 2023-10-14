﻿namespace BulletinBoard.Contracts.Categories
{
    /// <summary>
    /// Добавление категории.
    /// </summary>
    public class CreateCategoryDto
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid? ParentCategoryId { get; set; }
    }
}
