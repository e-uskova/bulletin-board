using System.ComponentModel.DataAnnotations;
using BulletinBoard.Contracts.Attributes;

namespace BulletinBoard.Contracts.Post
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePostDto 
    {
        /// <summary>
        /// Заголовок.
        /// </summary>
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Строка должна быть длиннее {2} и меньше {1} символов.")]
        public string Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Названия тегов.
        /// </summary>
        [TagsSizeAttribute(3, ErrorMessage = "Неверное количество тегов")]
        public string[] TagNames { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        [Range(0, 10_000, ErrorMessage = "Поле должно быть больше {1} и меньше {2}")]
        public decimal Price { get; set; }
    }
}
