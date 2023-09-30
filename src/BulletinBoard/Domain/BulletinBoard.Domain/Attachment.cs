namespace BulletinBoard.Domain
{
    /// <summary>
    /// Сущность вложения.
    /// </summary>
    public class Attachment : BaseEntity
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Содержимое в двоичном виде.
        /// </summary>
        public byte[] Data { get; set; }
    }
}
