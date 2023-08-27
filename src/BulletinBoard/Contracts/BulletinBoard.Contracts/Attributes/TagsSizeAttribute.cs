using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Contracts.Attributes
{
    /// <summary>
    /// Валидирует количество тегов.
    /// </summary>
    public class TagsSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;
        private readonly int _minSize;

        /// <summary>
        /// Инициализация экземпляра <see cref="TagsSizeAttribute"/>
        /// </summary>
        /// <param name="maxSize">Максимальное количество тегов.</param>
        /// <param name="minSize">Минимальное количество тегов.</param>
        public TagsSizeAttribute(int maxSize, int minSize = 1)
        {
            _maxSize = maxSize;
            _minSize = minSize;
        }

        /// <summary>
        /// Валидация длины массива по максимальной и минимальной.
        /// </summary>
        /// <param name="value">Поле для валидации.</param>
        /// <param name="validationContext">Контекст валидации.</param>
        /// <returns>Результат валидации.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) 
            {
                return ValidationResult.Success;
            }

            var tags = value as String[];
            if (tags.Length > _maxSize || tags.Length < _minSize) 
            {
                return new ValidationResult(ErrorMessage);            
            }

            return ValidationResult.Success;
        }
    }
}
