using BulletinBoard.Domain.Base;

namespace BulletinBoard.Domain.Categories
{
    /// <summary>
    /// Сущность категории.
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Дочерние категории.
        /// </summary>
        public IReadOnlyCollection<Category> Children { get; set;}

        /// <summary>
        /// Родительская категория.
        /// </summary>
        public Category Parent { get; set; }
    }
}
