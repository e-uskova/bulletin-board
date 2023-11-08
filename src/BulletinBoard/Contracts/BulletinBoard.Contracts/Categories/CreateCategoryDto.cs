using System.ComponentModel.DataAnnotations;

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
        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid? ParentCategoryId { get; set; }
    }
}
