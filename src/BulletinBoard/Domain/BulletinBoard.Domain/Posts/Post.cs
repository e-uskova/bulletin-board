using BulletinBoard.Domain.Base;
using BulletinBoard.Domain.Attachments;

namespace BulletinBoard.Domain.Posts
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
        /// Идентификатор категории.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Названия тегов.
        /// </summary>
        public string[] TagNames { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Вложения.
        /// </summary>
        public IReadOnlyCollection<Attachment> Attachments { get; set; }

        /// <summary>
        /// Адрес сделки.
        /// </summary>
        public string Address { get; set; }
    }
}