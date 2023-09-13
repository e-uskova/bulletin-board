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
        /// Идентификаторы дочерних категорий.
        /// </summary>
        public ICollection<Guid> SubCategoriesId { get; set;}

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid? ParentCategoryId { get; set; }
    }
}
