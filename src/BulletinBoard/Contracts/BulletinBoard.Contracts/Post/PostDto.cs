using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Contracts.Base;
using BulletinBoard.Contracts.Users;

namespace BulletinBoard.Contracts.Post
{
    /// <summary>
    /// Объявление.
    /// </summary>
    public class PostDto : BaseDto
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
        /// Название категории.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Изображения.
        /// </summary>
        public IReadOnlyCollection<AttachmentInfoDto> Attachments { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Автор объявления.
        /// </summary>
        public string AuthorName { get; set; }

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
