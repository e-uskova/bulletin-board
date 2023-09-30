namespace BulletinBoard.Domain
{
    /// <summary>
    /// Сущность комментария к объявлению.
    /// </summary>
    public class PostComment : BaseEntity
    {
        /// <summary>
        /// Автор.
        /// </summary>
        public User Author { get; set; }
        
        /// <summary>
        /// Текст.
        /// </summary>
        public string Text { get; set; }
    }
}
