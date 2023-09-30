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
        public string Name { get; set; }

        /// <summary>
        /// Путь к файлу.
        /// </summary>
        public string Path { get; set; }
    }
}
