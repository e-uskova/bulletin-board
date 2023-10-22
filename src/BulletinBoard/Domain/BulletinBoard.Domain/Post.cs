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
        public string? Description { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Вложения.
        /// </summary>
        public IReadOnlyCollection<Attachment> Attachments { get; set; }

        /// <summary>
        /// Автор объявления.
        /// </summary>
        public User Author { get; set; }

        /// <summary>
        /// Время создания.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Время последнего изменения.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Флаг активности объявления.
        /// </summary>
        public bool IsActive { get; set; }
    }
}