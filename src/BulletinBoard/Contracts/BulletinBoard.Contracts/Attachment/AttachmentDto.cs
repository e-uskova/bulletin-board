using BulletinBoard.Contracts.Base;

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
        /// Путь к файлу.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Тип контента.
        /// </summary>
        public string ContentType { get; set; }
    }
}
