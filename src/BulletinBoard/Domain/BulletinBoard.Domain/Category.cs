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
        /// Идентификатор родительской категории.
        /// </summary>
        public Category? ParentCategory { get; set; }
    }
}
