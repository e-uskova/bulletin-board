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
        public IReadOnlyCollection<AttachmentDto> Attachments { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Автор объявления.
        /// </summary>
        public UserDto Author { get; set; }

    }
}
