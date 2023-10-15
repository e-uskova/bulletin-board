using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Post
{
    /// <summary>
    /// Добавление объявления.
    /// </summary>
    public class CreatePostDto 
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Строка должна быть длиннее {2} и меньше {1} символов.")]
        public string Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        [Required]
        [Range(0, 1_000_000_000, ErrorMessage = "Поле должно быть больше {1} и меньше {2}")]
        public decimal Price { get; set; }
    }
}
