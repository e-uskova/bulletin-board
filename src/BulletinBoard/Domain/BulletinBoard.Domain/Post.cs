namespace BulletinBoard.Domain
{
    /// <summary>
    /// Сущность объявления.
    /// </summary>
    public class Post : BaseEntity
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        public Category CategoryId { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Вложения.
        /// </summary>
        public IReadOnlyCollection<Attachment> Attachments { get; set; }
    }
}