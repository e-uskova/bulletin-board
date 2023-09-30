namespace BulletinBoard.Domain
{
    /// <summary>
    /// Сущность категории.
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификаторы дочерних категорий.
        /// </summary>
        //public IReadOnlyCollection<Guid> SubсategoriesId { get; set;}

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid? ParentCategoryId { get; set; }
    }
}
