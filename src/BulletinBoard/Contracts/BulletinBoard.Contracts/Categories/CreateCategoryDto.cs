namespace BulletinBoard.Contracts.Categories
{
    /// <summary>
    /// Добавление категории.
    /// </summary>
    public class CreateCategoryDto
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Имя родительской категории.
        /// </summary>
        public String ParentCategoryName { get; set; }
    }
}
