﻿using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Categories
{
    /// <summary>
    /// Категория объявлений.
    /// </summary>
    public class CategoryDto : BaseDto
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
