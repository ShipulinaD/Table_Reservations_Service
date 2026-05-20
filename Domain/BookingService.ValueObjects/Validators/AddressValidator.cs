using BookingService.ValueObjects.Base;
using BookingService.ValueObjects.Exceptions;

namespace BookingService.ValueObjects.Validators;

/// <summary>
/// Определяет метод, реализующий проверку строки адреса.
/// </summary>
public class AddressValidator : IValidator<string>
{
    /// <summary>
    /// Максимальная длина адреса
    /// </summary>
    public static int MAX_LENGTH => 200;

    /// <summary>
    /// Минимальная длина адреса
    /// </summary>
    public static int MIN_LENGTH => 3;

    /// <summary>
    /// Проверяет строку адреса, чтобы убедиться, что она не null, не пустая и не состоит только из пробельных символов.
    /// </summary>
    /// <param name="value">Строка, содержащая адрес.</param>
    /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
    /// <exception cref="ArgumentLongValueException"></exception>
    /// <exception cref="ArgumentShortValueException"></exception>
    public void Validate(string value)
    {
        // Проверка: адрес не должен быть null, пустым или состоять только из пробелов
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        // Проверка: адрес не должен превышать максимальную длину
        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);

        // Проверка: адрес не должен быть короче минимальной длины
        if (value.Length < MIN_LENGTH)
            throw new ArgumentShortValueException(nameof(value), value, MIN_LENGTH);
    }
}