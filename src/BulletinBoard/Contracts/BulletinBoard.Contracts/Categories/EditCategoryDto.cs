using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Categories
{
    /// <summary>
    /// Изменение категории.
    /// </summary>
    public class EditCategoryDto
    {
        /// <summary>
        /// Название.
        /// </summary>
        [StringLength(64, MinimumLength = 2)]
        public string? Name { get; set; }

        /// <summary>
        /// Идентификатор родительской категории.
        /// </summary>
        public Guid? ParentCategoryId { get; set; }
    }
}
