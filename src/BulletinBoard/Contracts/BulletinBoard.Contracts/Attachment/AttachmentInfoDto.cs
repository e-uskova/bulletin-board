using BulletinBoard.Contracts.Base;

namespace BulletinBoard.Contracts.Attachment
{
    /// <summary>
    /// Информация о вложении без данных.
    /// </summary>
    public class AttachmentInfoDto : BaseDto
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип контента.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Размер файла.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Время создания.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Идентификатор объявления, к которому прикреплен файл.
        /// </summary>
        public Guid PostId { get; set; }
    }
}
