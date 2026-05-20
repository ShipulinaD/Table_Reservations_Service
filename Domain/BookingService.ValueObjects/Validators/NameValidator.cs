using BookingService.ValueObjects.Base;
using BookingService.ValueObjects.Exceptions;

namespace BookingService.ValueObjects.Validators;

/// <summary>
/// Определяет метод, реализующий проверку строки имени.
/// </summary>
public class NameValidator : IValidator<string>
{
    /// <summary>
    /// Максимальная длина имени
    /// </summary>
    public static int MAX_LENGTH => 100;

    /// <summary>
    /// Минимальная длина имени
    /// </summary>
    public static int MIN_LENGTH => 2;

    /// <summary>
    /// Проверяет строку, чтобы убедиться, что она не null, не пустая и не состоит только из пробельных символов.
    /// </summary>
    /// <param name="value">Строка, содержащая имя.</param>
    /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
    /// <exception cref="ArgumentLongValueException"></exception>
    /// <exception cref="ArgumentShortValueException"></exception>
    public void Validate(string value)
    {
        // Проверка: имя не должно быть null, пустым или состоять только из пробелов
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        // Проверка: имя не должно превышать максимальную длину
        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);

        // Проверка: имя не должно быть короче минимальной длины
        if (value.Length < MIN_LENGTH)
            throw new ArgumentShortValueException(nameof(value), value, MIN_LENGTH);
    }
}