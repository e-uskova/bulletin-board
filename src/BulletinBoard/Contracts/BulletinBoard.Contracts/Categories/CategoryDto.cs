﻿namespace BulletinBoard.Contracts.Categories
{
    /// <summary>
    /// Категория объявлений.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Идентификаторы дочерних категорий.
        /// </summary>
        public ICollection<Guid> SubCategoriesId { get; set; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid? ParentCategoryId { get; set; }
    }
}