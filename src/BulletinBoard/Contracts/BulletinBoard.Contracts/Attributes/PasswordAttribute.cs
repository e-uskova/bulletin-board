using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BulletinBoard.Contracts.Attributes
{
    /// <summary>
    /// Валидирует пароль.
    /// </summary>
    public class PasswordAttribute : ValidationAttribute
    {
        private readonly int _maxLength;
        private readonly int _minLength;
        private readonly bool _requireDigit;
        private readonly bool _requireDownCaseLetter;
        private readonly bool _requireUpperCaseLetter;
        private readonly bool _requireSpecialSymbols;

        /// <summary>
        /// Инициализация экземпляра <see cref="PasswordAttribute"/>
        /// </summary>
        /// <param name="maxLength">Максимальная длина.</param>
        /// <param name="minLength">Минимальная длина.</param>
        /// <param name="requireDigit">Должны быть цифры.</param>
        /// <param name="requireDownCaseLetter">Должны быть буквы нижнего регистра.</param>
        /// <param name="requireUpperCaseLetter">Должны быть буквы верхнего регистра.</param>
        /// <param name="requireSpecialSymbols">Должны быть спецсимволы.</param>
        public PasswordAttribute(
            int maxLength, 
            int minLength = 8, 
            bool requireDigit = false,
            bool requireDownCaseLetter = false, 
            bool requireUpperCaseLetter = false, 
            bool requireSpecialSymbols = false)
        {
            _maxLength = maxLength;
            _minLength = minLength;
            _requireDigit = requireDigit;
            _requireDownCaseLetter = requireDownCaseLetter;
            _requireUpperCaseLetter = requireUpperCaseLetter;
            _requireSpecialSymbols = requireSpecialSymbols;
        }

        /// <summary>
        /// Валидация пароля.
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

            var pass = (value as string).Trim();

            string errorText = $"Пароль должен содержать от {_minLength} до {_maxLength} символов";
            if (pass.Length > _maxLength || pass.Length < _minLength)
            {
                return new ValidationResult(errorText);
            }

            string regexString = "";
            List<string> errorTextRequires = new();

            if (_requireDigit)
            {
                regexString += "(?=.*[0-9])";
                errorTextRequires.Add("цифры");
            }
            if (_requireDownCaseLetter)
            {
                regexString += "(?=.*[a-z])";
                errorTextRequires.Add("строчные латинские буквы");
            }
            if (_requireUpperCaseLetter)
            {
                regexString += "(?=.*[A-Z])";
                errorTextRequires.Add("прописные латинские буквы");
            }
            if (_requireSpecialSymbols)
            {
                regexString += "(?=.*[!@#$%^&*])";
                errorTextRequires.Add("специальные символы (!@#$%^&*)");
            }
            regexString += $"[0-9a-zA-Z!@#$%^&*]{{{_minLength},}}";

            if (errorTextRequires.Count > 0)
            {
                errorText += $", в том числе {string.Join(", ", errorTextRequires)}";
            }

            var matches = Regex.Matches(pass, regexString);
            if (matches.Count == 0)
            {
                return new ValidationResult(errorText);
            }
            
            if (matches[0].Length == pass.Length)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Внутренняя ошибка");
        }
    }
}
