using BulletinBoard.Contracts.Base;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Attachment
{
    /// <summary>
    /// Вложение.
    /// </summary>
    public class AttachmentDto : BaseDto
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Контент.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Тип контента.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Идентификатор объявления, к которому прикреплен файл.
        /// </summary>
        [Required]
        public Guid PostId { get; set; }
    }
}
