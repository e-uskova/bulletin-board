using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Post
{
    /// <summary>
    /// Изменение объявления.
    /// </summary>
    public class EditPostDto
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Строка должна быть длиннее {2} и меньше {1} символов.")]
        public string? Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        [Range(0, 1_000_000_000, ErrorMessage = "Поле должно быть больше {1} и меньше {2}")]
        public decimal? Price { get; set; }
    }
}
