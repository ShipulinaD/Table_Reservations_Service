using System.Text.RegularExpressions;
using BookingService.ValueObjects.Base;
using BookingService.ValueObjects.Exceptions;

namespace BookingService.ValueObjects.Validators;

/// <summary>
/// Определяет метод, реализующий проверку строки телефонного номера
/// с использованием регулярных выражений.
/// </summary>
public class PhoneValidator : IValidator<string>
{
    /// <summary>
    /// Максимальное количество цифр в телефонном номере.
    /// </summary>
    public static int MAX_DIGITS => 15;

    /// <summary>
    /// Минимальное количество цифр в телефонном номере.
    /// </summary>
    public static int MIN_DIGITS => 10;

    /// <summary>
    /// Регулярное выражение для проверки формата телефонного номера.
    /// Допускает опциональный знак '+' в начале, цифры и разделители
    /// (пробелы, дефисы, круглые скобки).
    /// </summary>
    private static readonly Regex PhoneFormatRegex = new(
        @"^\+?[\d\s\-\(\)]+$",
        RegexOptions.Compiled);

    /// <summary>
    /// Регулярное выражение для извлечения только цифр из строки.
    /// </summary>
    private static readonly Regex DigitsOnlyRegex = new(
        @"\D",
        RegexOptions.Compiled);

    /// <summary>
    /// Проверяет строку телефонного номера на корректность формата
    /// с использованием регулярных выражений.
    /// </summary>
    /// <param name="value">Строка, содержащая номер телефона.</param>
    /// <exception cref="ArgumentNullOrWhiteSpaceException">
    /// Если значение равно null, пустое или состоит только из пробелов.
    /// </exception>
    /// <exception cref="InvalidPhoneFormatException">
    /// Если номер телефона имеет некорректный формат.
    /// </exception>
    public void Validate(string value)
    {
        // Проверка: номер телефона не должен быть null, пустым или состоять только из пробелов
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        // Проверка формата с помощью регулярного выражения:
        // разрешены только цифры, пробелы, дефисы, круглые скобки и '+' в начале
        if (!PhoneFormatRegex.IsMatch(value))
            throw new InvalidPhoneFormatException(nameof(value), value);

        // Извлекаем только цифры из строки (удаляем все нецифровые символы)
        var digitsOnly = DigitsOnlyRegex.Replace(value, string.Empty);

        // Проверка: количество цифр должно быть в допустимом диапазоне (от 10 до 15)
        if (digitsOnly.Length < MIN_DIGITS || digitsOnly.Length > MAX_DIGITS)
            throw new InvalidPhoneFormatException(nameof(value), value);
    }
}